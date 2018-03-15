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
        private int _lastID;

        public AddingScreen(Dataset dataset, int lastID)
        {
            InitializeComponent();
            _currentDataset = dataset;
            _lastID = lastID;
            SetDefaultDates();
        }

        private void SetDefaultDates()
        {
            tbIssueDate.Text = Clock.GetCurrentDate();
            tbPaymentDueDate.Text = Clock.GetCurrentDate();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string invoiceID = tbInvoiceID.Text;
            string issuedBy = tbIssuedBy.Text.ToUpper(); // convert to all-uppercase for standardization
            string fullSymbol = $"{tbMonthSymbol.Text}/{tbYearSymbol.Text}"; // passed by reference for validation
            DateTime issueDate = DateTime.Parse(tbIssueDate.Text);
            DateTime paymentDueDate = DateTime.Parse(tbPaymentDueDate.Text);
            string totalAmountCharged = tbTotalAmountCharged.Text; // also passed by reference for validation

            if (!ValidateBasicInput())
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!ValidateDates(tbIssueDate.Text, tbPaymentDueDate.Text))
            {
                MessageBox.Show("Błędny format daty. Wybierz datę z kalendarza.", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!ValidateSymbolInput(ref fullSymbol))
            {
                MessageBox.Show("Symbol musi być podany w formacie miesiąc/rok, np. 01/18.", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!ValidateAmountInput(ref totalAmountCharged))
            {
                MessageBox.Show("Błędny format kwoty. Wprowadź poprawne dane.", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            Invoice invoiceToAdd = new Invoice(_lastID + 1, invoiceID, issuedBy, fullSymbol, issueDate, paymentDueDate, totalAmountCharged);
            _currentDataset.Contents.Insert(0, invoiceToAdd);

            Close();
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
            if ((!String.IsNullOrWhiteSpace(tbPaymentDueDate.Text)) &&
                (DateTime.Compare(calPaymentDueDate.SelectionStart, DateTime.Parse(tbIssueDate.Text)) < 0))
            {
                MessageBox.Show("Zaznaczono datę wcześniejszą niż data wystawienia faktury. Wybierz poprawny termin płatności.",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                SetTextboxContent(tbPaymentDueDate, calPaymentDueDate.SelectionStart);
            }
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

        private bool ValidateSymbolInput (ref string symbolInput)
        {
            Regex symbolPatternValidFull = new Regex(@"^(0[1-9])|(1[0-2])/\d{2}$"); // 1-12 range for M(M), any 2-digit year
            Regex symbolPatternValidPartial = new Regex(@"^([1-9])/\d{2}$"); // 1-9 range for month variant

            if (symbolPatternValidPartial.IsMatch(symbolInput))
            {
                symbolInput = symbolInput.Insert(0, "0");
                return true;
            }
            else if (symbolPatternValidFull.IsMatch(symbolInput))
            {
                return true;
            }
            return false;
        }

        private bool ValidateAmountInput (ref string amountInput)
        {
            Regex plnOnlyPattern = new Regex(@"^\d+$");
            Regex incompleteDecimalPattern = new Regex(@"^\d+(\.|,)\d$");
            Regex fullPattern = new Regex(@"^\d+(\.|,)\d{2}$");

            if (plnOnlyPattern.IsMatch(amountInput))
            {
                amountInput += ",00";
                return true;
            }
            else if (incompleteDecimalPattern.IsMatch(amountInput))
            {
                ValidateDecimalSeparator(ref amountInput);
                amountInput += "0";
                return true;
            }
            else if (fullPattern.IsMatch(amountInput))
            {
                ValidateDecimalSeparator(ref amountInput);
                return true;
            }
            return false;
        }

        private void ValidateDecimalSeparator (ref string input)
        {
            Regex pattern = new Regex(@"\.\d{1,2}$");

            if (pattern.IsMatch(input))
            {
                input.Replace(".", ",");
            }
        }
    }
}
