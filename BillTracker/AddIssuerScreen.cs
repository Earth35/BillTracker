using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                _issuers.Add(tbAddIssuer.Text.ToUpper());
                Close();
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
