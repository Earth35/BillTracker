using System;
using System.ComponentModel;
using System.Windows.Forms;
using TrackerLogic;

namespace BillTracker
{
    public partial class AddingScreen : Form
    {
        private Dataset _currentDataset;
        private BindingList<string> _issuers;
        private int _lastID;

        public AddingScreen(Dataset dataset, int lastID)
        {
            InitializeComponent();
            _currentDataset = dataset;
            _issuers = new BindingList<string>();
            _lastID = lastID;
            SetDefaultDates();
            BindIssuersToDropdownMenu();
        }

        private void SetDefaultDates()
        {
            tbIssueDate.Text = Clock.GetCurrentDate();
            tbPaymentDueDate.Text = Clock.GetCurrentDate();
        }
        
        private void BindIssuersToDropdownMenu()
        {
            /* TODO
             * load issuers from XML/JSON file, populate the BindingList
             * then set the binding list as cbx datasource */
            cbxIssuedBy.DataSource = _issuers;
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
            string issuedBy = cbxIssuedBy.Text;
            string fullSymbol = $"{tbMonthSymbol.Text}/{tbYearSymbol.Text}"; // passed by reference for validation
            DateTime issueDate = DateTime.Parse(tbIssueDate.Text);
            DateTime paymentDueDate = DateTime.Parse(tbPaymentDueDate.Text);
            string totalAmountCharged = tbTotalAmountCharged.Text; // also passed by reference for validation

            Validator.Run(tbInvoiceNumber.Text, cbxIssuedBy.Text, tbIssueDate.Text, tbPaymentDueDate.Text,
                tbMonthSymbol.Text, tbYearSymbol.Text, tbTotalAmountCharged.Text, ref fullSymbol, ref totalAmountCharged);

            if (!Validator.BasicStatus)
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!Validator.DateStatus)
            {
                MessageBox.Show("Błędny format daty. Wybierz datę z kalendarza.", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!Validator.SymbolStatus)
            {
                MessageBox.Show("Symbol musi być podany w formacie miesiąc/rok, np. 01/18.", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!Validator.AmountStatus)
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

        private void btnNewIssuer_Click(object sender, EventArgs e)
        {
            AddIssuerScreen addIssuerScreen = new AddIssuerScreen(_issuers);
            addIssuerScreen.ShowDialog();
        }
    }
}
