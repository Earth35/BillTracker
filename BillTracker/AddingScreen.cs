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
using System.Text.RegularExpressions;

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
            if (!ValidateBasicInput())
            {
                MessageBox.Show("BASIC", "BASIC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!ValidateDates(tbIssueDate.Text, tbPaymentDueDate.Text))
            {
                MessageBox.Show("DATES", "DATES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!ValidateSymbolInput($"{tbMonthSymbol.Text}/{tbYearSymbol.Text}"))
            {
                MessageBox.Show("SYMBOL", "SYMBOL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!ValidateAmountInput(tbTotalAmountCharged.Text))
            {
                MessageBox.Show("AMOUNT", "AMOUNT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // pass values and close the dialog
            Close();
            /*
            
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
            
            */
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

        private bool ValidateBasicInput ()
        {
            List<bool> inputStatus = new List<bool>();
            inputStatus.Add(String.IsNullOrWhiteSpace(tbInvoiceID.Text));
            inputStatus.Add(String.IsNullOrWhiteSpace(tbIssuedBy.Text));
            inputStatus.Add(String.IsNullOrWhiteSpace(tbIssueDate.Text));
            inputStatus.Add(String.IsNullOrWhiteSpace(tbPaymentDueDate.Text));
            inputStatus.Add(String.IsNullOrWhiteSpace(tbMonthSymbol.Text));
            inputStatus.Add(String.IsNullOrWhiteSpace(tbYearSymbol.Text));
            inputStatus.Add(String.IsNullOrWhiteSpace(tbTotalAmountCharged.Text));

            if (inputStatus.All(s => s == false))
            {
                return true;
            }
            return false;
        }

        private bool ValidateDates (string issueDate, string paymentDeadline)
        {
            Regex format = new Regex(@"^\d{4}-\d{2}-\d{2}$");
            if ((format.IsMatch(issueDate) == true) && (format.IsMatch(paymentDeadline) == true))
            {
                return true;
            }
            return false;
        }

        private bool ValidateSymbolInput (string symbolInput)
        {
            Regex symbolPatternValidFull = new Regex(@"^(0[1-9])|(1[0-2])/\d{2}$"); // 1-12 range for M(M), any 2-digit year
            Regex symbolPatternValidPartial = new Regex(@"^([1-9])/\d{2}$"); // 1-9 range for month variant

            if (symbolPatternValidPartial.IsMatch(symbolInput))
            {
                tbMonthSymbol.Text = tbMonthSymbol.Text.Insert(0, "0");
                return true;
            }
            else if (symbolPatternValidFull.IsMatch(symbolInput))
            {
                return true;
            }
            return false;
        }

        private bool ValidateAmountInput (string amountInput)
        {
            Regex plnOnlyPattern = new Regex(@"^\d+$");
            Regex incompleteDecimalPattern = new Regex(@"^\d+(\.|,)\d$");
            Regex fullPattern = new Regex(@"^\d+(\.|,)\d{2}$");

            if (plnOnlyPattern.IsMatch(amountInput))
            {
                tbTotalAmountCharged.Text += ",00";
                return true;
            }
            else if (incompleteDecimalPattern.IsMatch(amountInput))
            {
                ValidateDecimalSeparator(amountInput);
                tbTotalAmountCharged.Text += "0";
                return true;
            }
            else if (fullPattern.IsMatch(amountInput))
            {
                ValidateDecimalSeparator(amountInput);
                return true;
            }
            return false;
        }

        private void ValidateDecimalSeparator (string input)
        {
            Regex pattern = new Regex(@"\.\d{1,2}$");

            if (pattern.IsMatch(input))
            {
                tbTotalAmountCharged.Text.Replace(".", ",");
            }
        }
    }
}
