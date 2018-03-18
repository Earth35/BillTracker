using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TrackerLogic
{
    public class Dataset
    {
        public BindingList<Invoice> Contents { get; set; }

        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + 
            $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}" + @"\DB\Database.mdf;" +
            "Integrated Security=True;Connect Timeout = 30";

        public Dataset()
        {
            Contents = LoadDataset();
        }

        private BindingList<Invoice> LoadDataset()
        {
            BindingList<Invoice> newSet = new BindingList<Invoice>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand readDataCommand = connection.CreateCommand())
                    {
                        readDataCommand.CommandType = CommandType.Text;
                        readDataCommand.CommandText = "SELECT * FROM InvoiceDataset";

                        SqlDataReader dataReader = readDataCommand.ExecuteReader();

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                int internalID = (int)dataReader["Id"];
                                string invoiceID = (string)dataReader["InvoiceID"];
                                string issuedBy = (string)dataReader["IssuedBy"];
                                string monthYearSymbol = (string)dataReader["MonthYearSymbol"];
                                DateTime issueDate = (DateTime)dataReader["IssueDate"];
                                DateTime paymentDueDate = (DateTime)dataReader["PaymentDueDate"];
                                string totalAmountCharged = (string)dataReader["TotalAmountCharged"];
                                var paymentDate = dataReader["PaymentDate"];

                                Invoice currentInvoice = new Invoice(internalID, invoiceID, issuedBy,
                                    monthYearSymbol, issueDate, paymentDueDate, totalAmountCharged);

                                if (paymentDate != DBNull.Value)
                                {
                                    currentInvoice.Pay(Convert.ToDateTime(paymentDate));
                                }

                                newSet.Insert(0, currentInvoice);
                            }
                        }
                        dataReader.Close();
                    }
                    return newSet;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }

            return null;
        }

        public void SaveDataset()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // delete existing rows
                    using (SqlCommand eraseDataCommand = connection.CreateCommand())
                    {
                        eraseDataCommand.CommandType = CommandType.Text;
                        eraseDataCommand.CommandText = "DELETE FROM InvoiceDataset";

                        eraseDataCommand.ExecuteNonQuery();
                    }
                    // reset IDs in the database
                    using (SqlCommand reseedCommand = connection.CreateCommand())
                    {
                        reseedCommand.CommandType = CommandType.Text;
                        reseedCommand.CommandText = "DBCC CHECKIDENT ('dbo.InvoiceDataset', RESEED, 0)";

                        reseedCommand.ExecuteNonQuery();
                    }

                    /*  insert updated entries
                        entries are currently in the "from latest to oldest" order
                        when re-inserted like this, the list would be in reverse order upon loading entries from the database
                        the list must be reversed before insertion  */

                    foreach (Invoice invoice in Contents.Reverse())
                    {
                        using (SqlCommand writeDataCommand = connection.CreateCommand())
                        {
                            writeDataCommand.CommandType = CommandType.Text;
                            writeDataCommand.CommandText =
                                "INSERT INTO InvoiceDataset " +
                                "(InvoiceId, IssuedBy, MonthYearSymbol, IssueDate, PaymentDueDate, TotalAmountCharged, PaymentDate) " +
                                "VALUES " +
                                "(@InvoiceId, @IssuedBy, @MonthYearSymbol, @IssueDate, @PaymentDueDate, @TotalAmountCharged, @PaymentDate)";

                            writeDataCommand.Parameters.Add("@InvoiceId", SqlDbType.Text);
                            writeDataCommand.Parameters["@InvoiceId"].Value = invoice.InvoiceNumber;
                            writeDataCommand.Parameters.Add("@IssuedBy", SqlDbType.VarChar);
                            writeDataCommand.Parameters["@IssuedBy"].Value = invoice.IssuedBy;
                            writeDataCommand.Parameters.Add("@MonthYearSymbol", SqlDbType.Char);
                            writeDataCommand.Parameters["@MonthYearSymbol"].Value = invoice.MonthYearSymbol;
                            writeDataCommand.Parameters.Add("@IssueDate", SqlDbType.Date);
                            writeDataCommand.Parameters["@IssueDate"].Value = invoice.IssueDate;
                            writeDataCommand.Parameters.Add("@PaymentDueDate", SqlDbType.Date);
                            writeDataCommand.Parameters["@PaymentDueDate"].Value = invoice.PaymentDueDate;
                            writeDataCommand.Parameters.Add("@TotalAmountCharged", SqlDbType.VarChar);
                            writeDataCommand.Parameters["@TotalAmountCharged"].Value = invoice.TotalAmountCharged;
                            writeDataCommand.Parameters.Add("@PaymentDate", SqlDbType.Date);

                            // Prevent default date/time value from being inserted into the database instead of NULL
                            if (invoice.PaidOn == Convert.ToDateTime("0001-01-01 00:00:00"))
                            {
                                writeDataCommand.Parameters["@PaymentDate"].Value = SqlDateTime.Null;
                            }
                            else
                            {
                                writeDataCommand.Parameters["@PaymentDate"].Value = invoice.PaidOn;
                            }
                            
                            writeDataCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
