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
        public AddIssuerScreen()
        {
            InitializeComponent();
        }

        private void btnAddIssuer_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Placeholder");
        }

        private void btnCancelAddingIssuer_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
