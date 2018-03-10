using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLogic;

namespace BillTracker
{
    public partial class MarkingScreen : Form
    {
        private Invoice _currentInvoice;

        public MarkingScreen(Invoice invoice)
        {
            InitializeComponent();
            _currentInvoice = invoice;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, calMarkingCalendar.SelectionStart) > 0 )
            {
                _currentInvoice.Pay(calMarkingCalendar.SelectionStart);
                Close();
            }
            else
            {
                MessageBox.Show("Nie można oznaczyć faktury jako opłaconej z datą przyszłą.", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
