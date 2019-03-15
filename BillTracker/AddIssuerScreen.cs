using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace BillTracker
{
    public partial class AddIssuerScreen : Form
    {
        private BindingList<string> _issuers = new BindingList<string>();

        public AddIssuerScreen(BindingList<string> issuers)
        {
            InitializeComponent();
            _issuers = issuers;
        }

        private void btnAddIssuer_Click(object sender, EventArgs e)
        {
            if (InputValid(tbAddIssuer.Text))
            {
                foreach (string x in _issuers)
                {
                    Console.WriteLine(x);
                }
                if (_issuers.Any(x => x == tbAddIssuer.Text.ToUpper()))
                {
                    MessageBox.Show("Obiekt już istnieje.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    _issuers.Add(tbAddIssuer.Text.ToUpper());
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Wprowadź nazwę wystawiającego.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool InputValid(string issuer)
        {
            if (!String.IsNullOrWhiteSpace(issuer))
            {
                return true;
            }
            return false;
        }

        private void btnCancelAddingIssuer_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
