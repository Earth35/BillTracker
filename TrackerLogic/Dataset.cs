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
        private BindingList<Invoice> _contents;

        public BindingList<Invoice> Contents
        {
            get { return _contents; }
            set
            {
                _contents = value;
            }
        }

        public Dataset ()
        {
            Contents = GenerateMockDataset();
        }

        // Generate mock dataset for testing purposes
        public BindingList<Invoice> GenerateMockDataset ()
        {
            BindingList<Invoice> newSet = new BindingList<Invoice>();

            for (int n = 0; n < 100; n++)
            {
                newSet.Insert(0, new Invoice($"Invoice_No. {n}", "Company X", "MM/YY", new DateTime(2018, 03, 09), new DateTime(2018, 03, 23), 99.99));
            }

            return newSet;
        }
    }
}
