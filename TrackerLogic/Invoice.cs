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
        public double TotalAmountCharged { get; set; }
        public bool IsPaid { get; private set; }
    }
}
