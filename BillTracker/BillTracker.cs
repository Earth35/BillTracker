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
        private Dataset _mockDataset;

        public BillTracker()
        {
            InitializeComponent();

            _mockDataset = new Dataset();
            _mockDataset.PropertyChanged += DatasetOnPropertyChanged;

            DisplayDateAndTime();
            UpdateInvoiceList();
            AdjustButtonColumnCells();
            dgvInvoiceList.DataSource = _mockDataset.Contents;
            dgvInvoiceList.CellContentClick += dgvInvoiceList_CellContentClick;
        }

        private void DisplayDateAndTime()
        {
            lblCurrentDateTime.Text = Clock.GetCurrentDateAndTime();
        }

        private void UpdateInvoiceList()
        {
            dgvInvoiceList.RowHeadersVisible = false;
            dgvInvoiceList.AutoGenerateColumns = false;
            dgvInvoiceList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvInvoiceList.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Numer faktury",
                Width = 175,
                DataPropertyName = "InvoiceID"
            });

            dgvInvoiceList.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Wystawiona przez",
                Width = 145,
                DataPropertyName = "IssuedBy"
            });

            dgvInvoiceList.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Symbol",
                Width = 50,
                DataPropertyName = "MonthYearSymbol"
            });

            dgvInvoiceList.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Data wystawienia",
                Width = 75,
                DataPropertyName = "IssueDate"
            });

            dgvInvoiceList.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Termin płatności",
                Width = 75,
                DataPropertyName = "PaymentDueDate"
            });

            dgvInvoiceList.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Data opłacenia",
                Width = 75,
                DataPropertyName = "PaidOn"
            });

            dgvInvoiceList.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Łączna kwota",
                Width = 60,
                DataPropertyName = "TotalAmountCharged"
            });

            dgvInvoiceList.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Opłacona?",
                Width = 75,
                DataPropertyName = "Status"
            });

            dgvInvoiceList.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "Oznacz jako opłacona",
                Width = 75,
                Text = "\u2713",
                UseColumnTextForButtonValue = true,
            });
        }

        private void AdjustButtonColumnCells()
        {
            dgvInvoiceList.Columns[8].DefaultCellStyle.Padding = new Padding(25, 0, 25, 0);
        }

        private void dgvInvoiceList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8 && e.RowIndex >= 0)
            {
                string invoiceID = (string)dgvInvoiceList.Rows[e.RowIndex].Cells[0].Value;
                Invoice selectedInvoice = _mockDataset.Contents.FirstOrDefault(i => i.InvoiceID == invoiceID);
                if (!selectedInvoice.IsPaid)
                {
                    _mockDataset.UpdateEntry(selectedInvoice);
                }
            }
        }

        private void tmrDateTime_Tick(object sender, EventArgs e)
        {
            DisplayDateAndTime();
        }

        private void DatasetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // obecnie zestaw danych zawiera wyłącznie jedną właściwość - jeśli to się nie zmieni, pomyśleć nad refaktoryzacją
            if (e.PropertyName == "FullDataset")
            {
                dgvInvoiceList.Refresh();
            }
        }
    }
}
