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

        public Dataset()
        {
            Contents = GenerateDataset();
        }

        public BindingList<Invoice> GenerateDataset()
        {
            BindingList<Invoice> newSet = new BindingList<Invoice>();

            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }

            return newSet;
        }
    }
}
