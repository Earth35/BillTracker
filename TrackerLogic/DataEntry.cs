using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLogic
{
    public class DataEntry : Invoice
    {
        public DataEntry(string invoiceID, string issuedBy, string monthYearSymbol, DateTime issueDate, DateTime paymentDueDate,
            DateTime paidOn, double totalAmountCharged) :
            base(invoiceID, issuedBy, monthYearSymbol, issueDate, paymentDueDate, paidOn, totalAmountCharged)
        {

        }
    }
}
