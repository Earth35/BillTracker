using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace TrackerLogic
{
    public class Dataset
    {
        public BindingList<Invoice> Contents { get; set; }

        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\C#\BillTracker\DB\Database.mdf;Integrated Security=True;Connect Timeout=30";

        public Dataset()
        {
            Contents = LoadDataset();
        }

        public BindingList<Invoice> LoadDataset()
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
                    // logic
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
