using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TrackerLogic
{
    public class Dataset
    {
        public BindingList<Invoice> FullDataset = new BindingList<Invoice>();

        public Dataset ()
        {
            GenerateMockDataset();
        }

        // Generate mock dataset for testing
        public void GenerateMockDataset ()
        {
            for (int n = 0; n < 100; n++)
            {
                FullDataset.Add(new Invoice($"Invoice_No. {n}", "Company X", "MM/YY", new DateTime(2018, 03, 09), new DateTime(2018, 03, 23), 99.99));
            }
        }
    }
}
