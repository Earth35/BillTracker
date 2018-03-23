using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLogic;

namespace BillTracker
{
    public partial class BillTracker : Form
    {
        #region constants
        private const int ID_COLUMN_INDEX = 0;
        private const int MARKING_BUTTON_COLUMN_INDEX = 9;
        private const int MARKING_FOR_DELETION_COLUMN_INDEX = 10;
        #endregion

        #region paginationVariables
        private Dataset _invoiceDataset;
        private int _currentDatasetSize;
        private int _numberOfPages;
        private int _currentPage = 0;
        private int _lastPageIndex;
        private List<BindingList<Invoice>> _subsetsOfData = new List<BindingList<Invoice>>();
        private BindingList<Invoice> _currentSubset = new BindingList<Invoice>();
        private int _numberOfSelectedInvoices = 0;
        #endregion

        public BillTracker()
        {
            InitializeComponent();

            _invoiceDataset = new Dataset();

            DisplayDateAndTime();
            BuildInvoiceListSchema();
            AdjustButtonColumnCells();
            Pagination.Set(_invoiceDataset, ref _currentDatasetSize, ref _numberOfPages, ref _lastPageIndex, _subsetsOfData);
            ViewCurrentSubset();
            dgvInvoiceList.DataSource = _currentSubset;
            dgvInvoiceList.CellContentClick += dgvInvoiceList_CellContentClick;
            DelayedInvoiceListRefresh();
        }

        private void DisplayDateAndTime()
        {
            lblCurrentDateTime.Text = Clock.GetCurrentDateAndTime();
        }

        private void BuildInvoiceListSchema()
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
                DataPropertyName = "InvoiceNumber"
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

            dgvInvoiceList.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Usuń",
                Width = 32,
                TrueValue = true,
                FalseValue = null,
                IndeterminateValue = null
                
            });
        }

        private void AdjustButtonColumnCells()
        {
            dgvInvoiceList.Columns[MARKING_BUTTON_COLUMN_INDEX].DefaultCellStyle.Padding = new Padding(25, 0, 25, 0);
        }

        private void ViewCurrentSubset()
        {
            _currentSubset.Clear();
            if (_currentDatasetSize > 0)
            {
                _currentSubset = _subsetsOfData[_currentPage];
                tbCurrentPage.Text = (_currentPage + 1).ToString();
            }
            ResetDatasource();
            SetVisibilityOfPagingControls();
        }

        private void ResetDatasource()
        {
            dgvInvoiceList.DataSource = typeof(BindingList<>);
            dgvInvoiceList.DataSource = _currentSubset;
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

        private void DelayedInvoiceListRefresh()
        {
            Task.Delay(TimeSpan.FromMilliseconds(250))
                .ContinueWith(task => HideObsoleteButtons()).ContinueWith(task => PaintRows());
        }

        private void HideObsoleteButtons()
        {
            // search through the dataset by invoice ID, then hide buttons in rows where IsPaid property is true
            foreach (DataGridViewRow row in dgvInvoiceList.Rows)
            {
                int internalID = (int)row.Cells[ID_COLUMN_INDEX].Value;

                if (_currentSubset.First(i => i.InternalID == internalID).IsPaid)
                {
                    DataGridViewCell cellToHide = row.Cells[MARKING_BUTTON_COLUMN_INDEX];
                    HideCell(cellToHide);
                }
            }
        }

        private void HideCell(DataGridViewCell cell)
        {
            // padding = cell width + 1 workaround
            DataGridViewCellStyle styleTypeHidden = new DataGridViewCellStyle();
            styleTypeHidden.Padding = new Padding(cell.Size.Width + 1, 0, 0, 0);
            cell.Style = styleTypeHidden;
        }

        private void ResetInvoiceListView()
        {
            Pagination.Set(_invoiceDataset, ref _currentDatasetSize, ref _numberOfPages, ref _lastPageIndex, _subsetsOfData);
            _currentPage = 0;
            ViewCurrentSubset();
            HideObsoleteButtons();
            PaintRows();
        }

        private void RefreshPagingControls()
        {
            ViewCurrentSubset();
            SetVisibilityOfPagingControls();
            DelayedInvoiceListRefresh();
        }

        private void ToggleDeleteButton()
        {
            if (_numberOfSelectedInvoices == 0) { btnDeleteSelected.Enabled = false; }
            else { btnDeleteSelected.Enabled = true; }
        }

        private void ResetSelection()
        {
            _numberOfSelectedInvoices = 0;
            ToggleDeleteButton();
        }

        private void PaintRows()
        {
            foreach (DataGridViewRow row in dgvInvoiceList.Rows)
            {
                StatusPainter.PaintRow(row);
            }
        }

        private void InvoiceOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Status")
            {
                ResetDatasource();
                HideObsoleteButtons();
                PaintRows();
            }
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
            else if (e.ColumnIndex == MARKING_FOR_DELETION_COLUMN_INDEX && e.RowIndex >= 0)
            {
                DataGridViewCheckBoxCell currentCell =
                    (DataGridViewCheckBoxCell)dgvInvoiceList.Rows[e.RowIndex].Cells[MARKING_FOR_DELETION_COLUMN_INDEX];

                if (currentCell.Value == currentCell.TrueValue)
                {
                    currentCell.Value = currentCell.FalseValue;
                    _numberOfSelectedInvoices--;
                }
                else
                {
                    currentCell.Value = currentCell.TrueValue;
                    _numberOfSelectedInvoices++;
                }
                ToggleDeleteButton();
            }
        }

        private void tmrDateTime_Tick(object sender, EventArgs e)
        {
            DisplayDateAndTime();
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            _currentPage = 0;
            ResetSelection();
            RefreshPagingControls();
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            _currentPage -= 1;
            if (_currentPage < 0) { _currentPage = 0; } // guard clause
            ResetSelection();
            RefreshPagingControls();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            _currentPage++;
            if (_currentPage > _lastPageIndex) { _currentPage = _lastPageIndex; } // guard clause
            ResetSelection();
            RefreshPagingControls();
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            _currentPage = _lastPageIndex;
            ResetSelection();
            RefreshPagingControls();
        }

        private void tbCurrentPage_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbCurrentPage.Text, out int indexOfPageToRead))
            {
                indexOfPageToRead--;
                if (indexOfPageToRead >= 0 && indexOfPageToRead <= _lastPageIndex)
                {
                    _currentPage = indexOfPageToRead;
                    RefreshPagingControls();
                }
                else
                {
                    MessageBox.Show("Wskazana strona nie istnieje.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
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
            ResetInvoiceListView();
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            DialogResult choice = MessageBox.Show($"Czy na pewno chcesz usunąć zaznaczone faktury ({_numberOfSelectedInvoices})?",
                "Usuwanie zaznaczonych pozycji", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (choice == DialogResult.Yes)
            {
                int numberOfDgvRows = dgvInvoiceList.RowCount;
                for (int n=0; n<numberOfDgvRows; n++)
                {
                    var markedForDeletion = dgvInvoiceList.Rows[n].Cells[MARKING_FOR_DELETION_COLUMN_INDEX].Value;
                    if (markedForDeletion != null)
                    {
                        int currentID = (int)dgvInvoiceList.Rows[n].Cells[ID_COLUMN_INDEX].Value;
                        Invoice invoiceToDelete = _invoiceDataset.Contents.First(i => i.InternalID == currentID);
                        _invoiceDataset.Contents.Remove(invoiceToDelete);
                    }
                }
            }
            ResetInvoiceListView();
        }

        private void BillTracker_FormClosing(object sender, FormClosingEventArgs e)
        {
            _invoiceDataset.SaveDataset();
        }
    }
}
