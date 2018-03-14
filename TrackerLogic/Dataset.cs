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
                    // logic
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }

            return newSet;
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
