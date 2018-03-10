﻿using System;
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
                selectedInvoice.PropertyChanged += InvoiceOnPropertyChanged;
                if (!selectedInvoice.IsPaid)
                {
                    MarkingScreen markingScreen = new MarkingScreen(selectedInvoice);
                    markingScreen.ShowDialog(this);
                }
                selectedInvoice.PropertyChanged -= InvoiceOnPropertyChanged;
            }
        }

        private void HideCell (DataGridViewCell cell)
        {
            // padding = cell width + 1 workaround
            DataGridViewCellStyle styleTypeHidden = new DataGridViewCellStyle();
            styleTypeHidden.Padding = new Padding( cell.Size.Width + 1, 0, 0, 0);
            cell.Style = styleTypeHidden;
        }

        private void HideObsoleteButtons ()
        {
            // search through the dataset by invoiceID, then hide buttons in rows where IsPaid property is true
            foreach (DataGridViewRow row in dgvInvoiceList.Rows)
            {
                string invoiceID = row.Cells[0].Value as string;
                
                if (_mockDataset.Contents.First(i => i.InvoiceID == invoiceID).IsPaid)
                {
                    DataGridViewCell cellToHide = row.Cells[row.Cells.Count - 1];
                    HideCell(cellToHide);
                }
            }
        }

        private void tmrDateTime_Tick(object sender, EventArgs e)
        {
            DisplayDateAndTime();
        }

        private void InvoiceOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Status")
            {
                // reset DataSource
                dgvInvoiceList.DataSource = typeof(BindingList<>);
                dgvInvoiceList.DataSource = _mockDataset.Contents;
                HideObsoleteButtons();
            }
        }
    }
}
