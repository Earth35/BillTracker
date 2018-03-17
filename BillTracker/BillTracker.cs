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
        private const int ID_COLUMN_INDEX = 0;
        private const int MARKING_BUTTON_COLUMN_INDEX = 9;
        private const int NUMBER_OF_RECORDS_PER_PAGE = 20;
        private Dataset _invoiceDataset;
        // pagination
        private int _currentDatasetSize;
        private int _numberOfPages;
        private int _currentPage = 0;
        private int _lastPageIndex;
        private List<BindingList<Invoice>> _subsetsOfData = new List<BindingList<Invoice>>();
        private BindingList<Invoice> _currentSubset = new BindingList<Invoice>();

        public BillTracker()
        {
            InitializeComponent();

            _invoiceDataset = new Dataset();

            DisplayDateAndTime();
            UpdateInvoiceList();
            AdjustButtonColumnCells();
            AddPagination();
            dgvInvoiceList.DataSource = _currentSubset;
            dgvInvoiceList.CellContentClick += dgvInvoiceList_CellContentClick;
            DelayedInvoiceListRefresh();
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
                DataPropertyName = "InternalID",
                Visible = false
            });

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
            dgvInvoiceList.Columns[MARKING_BUTTON_COLUMN_INDEX].DefaultCellStyle.Padding = new Padding(25, 0, 25, 0);
        }

        private void dgvInvoiceList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == MARKING_BUTTON_COLUMN_INDEX && e.RowIndex >= 0)
            {
                int internalID = (int)dgvInvoiceList.Rows[e.RowIndex].Cells[ID_COLUMN_INDEX].Value;
                Invoice selectedInvoice = _invoiceDataset.Contents.FirstOrDefault(i => i.InternalID == internalID);
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
            styleTypeHidden.Padding = new Padding(cell.Size.Width + 1, 0, 0, 0);
            cell.Style = styleTypeHidden;
        }

        private void HideObsoleteButtons ()
        {
            // search through the dataset by invoiceID, then hide buttons in rows where IsPaid property is true
            foreach (DataGridViewRow row in dgvInvoiceList.Rows)
            {
                int internalID = (int)row.Cells[ID_COLUMN_INDEX].Value;
                
                if (_currentSubset.First(i => i.InternalID == internalID).IsPaid)
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
                ResetDatasource();
                HideObsoleteButtons();
            }
        }

        private void ResetDatasource ()
        {
            dgvInvoiceList.DataSource = typeof(BindingList<>);
            dgvInvoiceList.DataSource = _currentSubset;
        }

        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            int lastID;
            if (dgvInvoiceList.Rows.Count != 0)
            {
                lastID = (int)dgvInvoiceList.Rows[0].Cells[ID_COLUMN_INDEX].Value;
            }
            else
            {
                lastID = 0; // first row always contains the latest invoice
            }
            AddingScreen addingScreen = new AddingScreen(_invoiceDataset, lastID);
            addingScreen.ShowDialog();
            AddPagination();
            _currentPage = 0;
            SetCurrentSubset();
        }

        private void DelayedInvoiceListRefresh()
        {
            Task.Delay(TimeSpan.FromMilliseconds(250)).ContinueWith(task => HideObsoleteButtons());
        }

        private void BillTracker_FormClosing(object sender, FormClosingEventArgs e)
        {
            _invoiceDataset.SaveDataset();
        }

        private void AddPagination()
        {
            _subsetsOfData.Clear();
            _currentDatasetSize = _invoiceDataset.Contents.Count();
            if (_currentDatasetSize == 0)
            {
                _numberOfPages = 1;
            }
            else
            {
                if (_currentDatasetSize % NUMBER_OF_RECORDS_PER_PAGE == 0)
                {
                    _numberOfPages = _currentDatasetSize / NUMBER_OF_RECORDS_PER_PAGE;
                }
                else
                {
                    _numberOfPages = (_currentDatasetSize / NUMBER_OF_RECORDS_PER_PAGE) + 1;
                }
            }
            _lastPageIndex = _numberOfPages - 1;

            for (int n = 0; n < _numberOfPages; n++)
            {
                List<Invoice> currentSubset =
                    _invoiceDataset.Contents.Skip(n * NUMBER_OF_RECORDS_PER_PAGE).Take(NUMBER_OF_RECORDS_PER_PAGE).ToList();
                BindingList<Invoice> bindingSubset = new BindingList<Invoice>(currentSubset);

                _subsetsOfData.Add(bindingSubset);
            }
            if (_currentDatasetSize > 0)
            {
                SetCurrentSubset();
            }
            SetVisibilityOfPagingControls();
        }

        private void SetCurrentSubset ()
        {
            _currentSubset = _subsetsOfData[_currentPage];
            tbCurrentPage.Text = (_currentPage + 1).ToString();
            ResetDatasource();
        }

        private void SetVisibilityOfPagingControls()
        {
            lblNumberOfPages.Text = $"/ {_numberOfPages.ToString()}";

            if (_currentDatasetSize == 0 || (_currentPage == 0 && _currentPage == _lastPageIndex))
            {
                btnFirstPage.Visible = false;
                btnPreviousPage.Visible = false;
                btnNextPage.Visible = false;
                btnLastPage.Visible = false;
            }
            else if (_currentPage == 0)
            {
                btnFirstPage.Visible = false;
                btnPreviousPage.Visible = false;
                btnNextPage.Visible = true;
                btnLastPage.Visible = true;
            }
            else if (_currentPage == _lastPageIndex)
            {
                btnFirstPage.Visible = true;
                btnPreviousPage.Visible = true;
                btnNextPage.Visible = false;
                btnLastPage.Visible = false;
            }
            else
            {
                btnFirstPage.Visible = true;
                btnPreviousPage.Visible = true;
                btnNextPage.Visible = true;
                btnLastPage.Visible = true;
            }
        }

        private void RefreshPagingControls()
        {
            SetCurrentSubset();
            SetVisibilityOfPagingControls();
            DelayedInvoiceListRefresh();
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            _currentPage = 0;
            RefreshPagingControls();
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            _currentPage -= 1;
            if (_currentPage < 0) { _currentPage = 0; } // guard clause
            RefreshPagingControls();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            _currentPage++;
            if (_currentPage > _lastPageIndex) { _currentPage = _lastPageIndex; } // guard clause
            RefreshPagingControls();
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            _currentPage = _lastPageIndex;
            RefreshPagingControls();
        }

        private void tbCurrentPage_TextChanged(object sender, EventArgs e)
        {
            int readPage = 0;

            if (int.TryParse(tbCurrentPage.Text, out readPage))
            {
                readPage--;
                if (readPage >= 0 && readPage <= _lastPageIndex)
                {
                    _currentPage = readPage;
                    RefreshPagingControls();
                }
                else
                {
                    MessageBox.Show("Wskazana strona nie istnieje.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}
