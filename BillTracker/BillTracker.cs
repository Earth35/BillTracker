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
    public partial class BillTracker : Form
    {
        public BillTracker()
        {
            InitializeComponent();

            DisplayDateAndTime();
        }

        private void DisplayDateAndTime()
        {
            lblCurrentDateTime.Text = Clock.GetCurrentDateAndTime();
        }

        private void tmrDateTime_Tick(object sender, EventArgs e)
        {
            DisplayDateAndTime();
        }
    }
}
