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
using System.Globalization;

namespace BillTracker
{
    public partial class AddingScreen : Form
    {
        private Dataset _currentDataset;

        public AddingScreen(Dataset dataset)
        {
            InitializeComponent();
            _currentDataset = dataset;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // TODO Sanitize input - make sure everything is valid and necessary fields weren't left empty
            SanitizeInput();
            // Pass invoice params and push invoice into the dataset
            string invoiceId = tbInvoiceID.Text;
            string issuedBy = tbIssuedBy.Text;
            // TODO - sanitize the symbol
            string symbol = tbMonthSymbol.Text + tbYearSymbol.Text;
            // TODO - handle the time portion somewhere
            DateTime issueDate = DateTime.Parse(tbIssueDate.Text);
            DateTime paymentDueDate = DateTime.Parse(tbPaymentDueDate.Text);
            // TODO - sanitize input, dot recognized as decimal separator, comma only serves as a "visual" separator;
            // Also, round it to 2 decimal points
            double totalAmountCharged = Double.Parse(tbTotalAmountCharged.Text, CultureInfo.InvariantCulture);

            // create a new invoice and push it into the dataset
        }

        private void calIssueDate_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (DateTime.Compare(calIssueDate.SelectionStart, DateTime.Now) > 0)
            {
                MessageBox.Show("Zaznaczono datę przyszłą. Wybierz poprawną datę, z którą została wystawiona faktura.",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!String.IsNullOrWhiteSpace(tbPaymentDueDate.Text) &&
                DateTime.Compare(calIssueDate.SelectionStart, DateTime.Parse(tbPaymentDueDate.Text)) > 0)
            {
                MessageBox.Show("Zaznaczono datę poźniejszą niż termin płatności. Wybierz poprawną datę, z którą została wystawiona faktura.",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                SetTextboxContent(tbIssueDate, calIssueDate.SelectionStart);
            }
        }

        private void calPaymentDueDate_DateChanged(object sender, DateRangeEventArgs e)
        {
            SetTextboxContent(tbPaymentDueDate, calPaymentDueDate.SelectionStart);
        }

        private void SetTextboxContent(TextBox textbox, DateTime date)
        {
            textbox.Text = date.ToShortDateString();
        }

        private void SanitizeInput()
        {
            // WIP
        }
    }
}
