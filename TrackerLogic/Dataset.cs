using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TrackerLogic
{
    public class Dataset : INotifyPropertyChanged
    {
        private BindingList<Invoice> _invoiceList;

        public BindingList<Invoice> InvoiceList
        {
            get { return _invoiceList; }
            set
            {
                _invoiceList = value;
                OnPropertyChanged("FullDataset");
            }
        }

        public Dataset ()
        {
            InvoiceList = GenerateMockDataset();
        }

        // Generate mock dataset for testing
        public BindingList<Invoice> GenerateMockDataset ()
        {
            BindingList<Invoice> newSet = new BindingList<Invoice>();

            for (int n = 0; n < 100; n++)
            {
                newSet.Add(new Invoice($"Invoice_No. {n}", "Company X", "MM/YY", new DateTime(2018, 03, 09), new DateTime(2018, 03, 23), 99.99));
            }

            return newSet;
        }

        public void UpdateEntry(Invoice entry)
        {
            Invoice selectedEntry = InvoiceList.FirstOrDefault(i => i.InvoiceID == entry.InvoiceID);
            selectedEntry.Pay(DateTime.Now);
            OnPropertyChanged("FullDataset");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged (string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
