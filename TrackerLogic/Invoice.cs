using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLogic
{
    public class Invoice
    {
        public string InvoiceID { get; set; }
        public string IssuedBy { get; set; }
        public string MonthYearSymbol { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public DateTime PaidOn { get; set; }
        public double TotalAmountCharged { get; set; }
        public bool IsPaid { get; private set; }
        public string Status { get; private set; }

        public Invoice (string invoiceID, string issuedBy, string monthYearSymbol, DateTime issueDate,
            DateTime paymentDueDate, double totalAmountCharged)
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
            PaidOn = paymentDate;
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
    }
}
