using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TrackerLogic
{
    public class Invoice : INotifyPropertyChanged
    {
        private bool _isPaid;

        public string InvoiceID { get; set; }
        public string IssuedBy { get; set; }
        public string MonthYearSymbol { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public DateTime PaidOn { get; set; }
        public string TotalAmountCharged { get; set; }
        public bool IsPaid
        {
            get { return _isPaid; }
            set
            {
                _isPaid = value;
                OnPropertyChanged("Status");
            }
        }
        public string Status { get; private set; }

        public Invoice (string invoiceID, string issuedBy, string monthYearSymbol, DateTime issueDate,
            DateTime paymentDueDate, string totalAmountCharged)
        {
            InvoiceID = invoiceID;
            IssuedBy = issuedBy;
            MonthYearSymbol = monthYearSymbol;
            IssueDate = issueDate;
            PaymentDueDate = paymentDueDate;
            TotalAmountCharged = totalAmountCharged;
            IsPaid = false;
            SetStatus();
        }

        public void Pay(DateTime paymentDate)
        {
            PaidOn = paymentDate.Date;
            IsPaid = true;
            SetStatus();
        }

        private void SetStatus()
        {
            if (!IsPaid)
            {
                Status = "";
            }
            else
            {
                Status = "Opłacona";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
