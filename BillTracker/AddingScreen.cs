using System;
using System.Windows.Forms;
using TrackerLogic;

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
        
        private void SetTextboxContent(TextBox textbox, DateTime date)
        {
            textbox.Text = date.ToShortDateString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string invoiceID = tbInvoiceNumber.Text;
            string issuedBy = tbIssuedBy.Text.ToUpper(); // convert to all-uppercase for standardization
            string fullSymbol = $"{tbMonthSymbol.Text}/{tbYearSymbol.Text}"; // passed by reference for validation
            DateTime issueDate = DateTime.Parse(tbIssueDate.Text);
            DateTime paymentDueDate = DateTime.Parse(tbPaymentDueDate.Text);
            string totalAmountCharged = tbTotalAmountCharged.Text; // also passed by reference for validation

            if (!Validator.RunBasicValidation(tbInvoiceNumber.Text, tbIssuedBy.Text, tbIssueDate.Text, tbPaymentDueDate.Text,
                tbMonthSymbol.Text, tbYearSymbol.Text, tbTotalAmountCharged.Text))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!Validator.RunDateValidation(tbIssueDate.Text, tbPaymentDueDate.Text))
            {
                MessageBox.Show("Błędny format daty. Wybierz datę z kalendarza.", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!Validator.RunSymbolValidation(ref fullSymbol))
            {
                MessageBox.Show("Symbol musi być podany w formacie miesiąc/rok, np. 01/18.", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!Validator.RunAmountValidation(ref totalAmountCharged))
            {
                MessageBox.Show("Błędny format kwoty. Wprowadź poprawne dane.", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            Invoice invoiceToAdd = new Invoice(_lastID + 1, invoiceID, issuedBy, fullSymbol,
                issueDate, paymentDueDate, totalAmountCharged);

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
    }
}
