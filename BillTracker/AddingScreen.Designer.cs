namespace BillTracker
{
    partial class AddingScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblInvoiceID = new System.Windows.Forms.Label();
            this.lblIssuedBy = new System.Windows.Forms.Label();
            this.lblMonthYearSymbol = new System.Windows.Forms.Label();
            this.lblIssueDate = new System.Windows.Forms.Label();
            this.lblPaymentDueDate = new System.Windows.Forms.Label();
            this.lblTotalAmountCharged = new System.Windows.Forms.Label();
            this.tbInvoiceID = new System.Windows.Forms.TextBox();
            this.tbIssuedBy = new System.Windows.Forms.TextBox();
            this.tbMonthSymbol = new System.Windows.Forms.TextBox();
            this.tbYearSymbol = new System.Windows.Forms.TextBox();
            this.tbTotalAmountCharged = new System.Windows.Forms.TextBox();
            this.tbIssueDate = new System.Windows.Forms.TextBox();
            this.tbPaymentDueDate = new System.Windows.Forms.TextBox();
            this.calIssueDate = new System.Windows.Forms.MonthCalendar();
            this.calPaymentDueDate = new System.Windows.Forms.MonthCalendar();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTitle.Location = new System.Drawing.Point(307, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(105, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dodaj fakturę";
            // 
            // lblInvoiceID
            // 
            this.lblInvoiceID.AutoSize = true;
            this.lblInvoiceID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblInvoiceID.Location = new System.Drawing.Point(51, 56);
            this.lblInvoiceID.Name = "lblInvoiceID";
            this.lblInvoiceID.Size = new System.Drawing.Size(101, 17);
            this.lblInvoiceID.TabIndex = 1;
            this.lblInvoiceID.Text = "Numer faktury:";
            this.lblInvoiceID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIssuedBy
            // 
            this.lblIssuedBy.AutoSize = true;
            this.lblIssuedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblIssuedBy.Location = new System.Drawing.Point(26, 159);
            this.lblIssuedBy.Name = "lblIssuedBy";
            this.lblIssuedBy.Size = new System.Drawing.Size(126, 17);
            this.lblIssuedBy.TabIndex = 2;
            this.lblIssuedBy.Text = "Wystawiona przez:";
            this.lblIssuedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMonthYearSymbol
            // 
            this.lblMonthYearSymbol.AutoSize = true;
            this.lblMonthYearSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblMonthYearSymbol.Location = new System.Drawing.Point(26, 262);
            this.lblMonthYearSymbol.Name = "lblMonthYearSymbol";
            this.lblMonthYearSymbol.Size = new System.Drawing.Size(126, 17);
            this.lblMonthYearSymbol.TabIndex = 3;
            this.lblMonthYearSymbol.Text = "Symbol (MM / RR):";
            this.lblMonthYearSymbol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIssueDate
            // 
            this.lblIssueDate.AutoSize = true;
            this.lblIssueDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblIssueDate.Location = new System.Drawing.Point(381, 56);
            this.lblIssueDate.Name = "lblIssueDate";
            this.lblIssueDate.Size = new System.Drawing.Size(120, 17);
            this.lblIssueDate.TabIndex = 4;
            this.lblIssueDate.Text = "Data wystawienia:";
            this.lblIssueDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaymentDueDate
            // 
            this.lblPaymentDueDate.AutoSize = true;
            this.lblPaymentDueDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPaymentDueDate.Location = new System.Drawing.Point(385, 263);
            this.lblPaymentDueDate.Name = "lblPaymentDueDate";
            this.lblPaymentDueDate.Size = new System.Drawing.Size(116, 17);
            this.lblPaymentDueDate.TabIndex = 5;
            this.lblPaymentDueDate.Text = "Termin płatności:";
            this.lblPaymentDueDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAmountCharged
            // 
            this.lblTotalAmountCharged.AutoSize = true;
            this.lblTotalAmountCharged.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTotalAmountCharged.Location = new System.Drawing.Point(41, 365);
            this.lblTotalAmountCharged.Name = "lblTotalAmountCharged";
            this.lblTotalAmountCharged.Size = new System.Drawing.Size(111, 17);
            this.lblTotalAmountCharged.TabIndex = 6;
            this.lblTotalAmountCharged.Text = "Całkowita kwota:";
            this.lblTotalAmountCharged.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbInvoiceID
            // 
            this.tbInvoiceID.Location = new System.Drawing.Point(158, 55);
            this.tbInvoiceID.Name = "tbInvoiceID";
            this.tbInvoiceID.Size = new System.Drawing.Size(175, 20);
            this.tbInvoiceID.TabIndex = 7;
            // 
            // tbIssuedBy
            // 
            this.tbIssuedBy.Location = new System.Drawing.Point(158, 158);
            this.tbIssuedBy.Name = "tbIssuedBy";
            this.tbIssuedBy.Size = new System.Drawing.Size(175, 20);
            this.tbIssuedBy.TabIndex = 8;
            // 
            // tbMonthSymbol
            // 
            this.tbMonthSymbol.Location = new System.Drawing.Point(158, 261);
            this.tbMonthSymbol.MaxLength = 2;
            this.tbMonthSymbol.Name = "tbMonthSymbol";
            this.tbMonthSymbol.Size = new System.Drawing.Size(50, 20);
            this.tbMonthSymbol.TabIndex = 9;
            this.tbMonthSymbol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbYearSymbol
            // 
            this.tbYearSymbol.Location = new System.Drawing.Point(232, 261);
            this.tbYearSymbol.MaxLength = 2;
            this.tbYearSymbol.Name = "tbYearSymbol";
            this.tbYearSymbol.Size = new System.Drawing.Size(50, 20);
            this.tbYearSymbol.TabIndex = 10;
            this.tbYearSymbol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbTotalAmountCharged
            // 
            this.tbTotalAmountCharged.Location = new System.Drawing.Point(158, 364);
            this.tbTotalAmountCharged.Name = "tbTotalAmountCharged";
            this.tbTotalAmountCharged.Size = new System.Drawing.Size(175, 20);
            this.tbTotalAmountCharged.TabIndex = 11;
            // 
            // tbIssueDate
            // 
            this.tbIssueDate.Location = new System.Drawing.Point(507, 55);
            this.tbIssueDate.MaxLength = 10;
            this.tbIssueDate.Name = "tbIssueDate";
            this.tbIssueDate.Size = new System.Drawing.Size(175, 20);
            this.tbIssueDate.TabIndex = 12;
            // 
            // tbPaymentDueDate
            // 
            this.tbPaymentDueDate.Location = new System.Drawing.Point(507, 262);
            this.tbPaymentDueDate.MaxLength = 10;
            this.tbPaymentDueDate.Name = "tbPaymentDueDate";
            this.tbPaymentDueDate.Size = new System.Drawing.Size(175, 20);
            this.tbPaymentDueDate.TabIndex = 13;
            // 
            // calIssueDate
            // 
            this.calIssueDate.Location = new System.Drawing.Point(525, 78);
            this.calIssueDate.Name = "calIssueDate";
            this.calIssueDate.TabIndex = 14;
            // 
            // calPaymentDueDate
            // 
            this.calPaymentDueDate.Location = new System.Drawing.Point(525, 285);
            this.calPaymentDueDate.Name = "calPaymentDueDate";
            this.calPaymentDueDate.TabIndex = 15;
            // 
            // lblSeparator
            // 
            this.lblSeparator.AutoSize = true;
            this.lblSeparator.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblSeparator.Location = new System.Drawing.Point(214, 262);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(12, 17);
            this.lblSeparator.TabIndex = 16;
            this.lblSeparator.Text = "/";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(258, 424);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Anuluj";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(360, 424);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 18;
            this.btnSubmit.Text = "Zatwierdź";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // AddingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 471);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblSeparator);
            this.Controls.Add(this.calPaymentDueDate);
            this.Controls.Add(this.calIssueDate);
            this.Controls.Add(this.tbPaymentDueDate);
            this.Controls.Add(this.tbIssueDate);
            this.Controls.Add(this.tbTotalAmountCharged);
            this.Controls.Add(this.tbYearSymbol);
            this.Controls.Add(this.tbMonthSymbol);
            this.Controls.Add(this.tbIssuedBy);
            this.Controls.Add(this.tbInvoiceID);
            this.Controls.Add(this.lblTotalAmountCharged);
            this.Controls.Add(this.lblPaymentDueDate);
            this.Controls.Add(this.lblIssueDate);
            this.Controls.Add(this.lblMonthYearSymbol);
            this.Controls.Add(this.lblIssuedBy);
            this.Controls.Add(this.lblInvoiceID);
            this.Controls.Add(this.lblTitle);
            this.Name = "AddingScreen";
            this.Text = "Dodaj fakturę";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInvoiceID;
        private System.Windows.Forms.Label lblIssuedBy;
        private System.Windows.Forms.Label lblMonthYearSymbol;
        private System.Windows.Forms.Label lblIssueDate;
        private System.Windows.Forms.Label lblPaymentDueDate;
        private System.Windows.Forms.Label lblTotalAmountCharged;
        private System.Windows.Forms.TextBox tbInvoiceID;
        private System.Windows.Forms.TextBox tbIssuedBy;
        private System.Windows.Forms.TextBox tbMonthSymbol;
        private System.Windows.Forms.TextBox tbYearSymbol;
        private System.Windows.Forms.TextBox tbTotalAmountCharged;
        private System.Windows.Forms.TextBox tbIssueDate;
        private System.Windows.Forms.TextBox tbPaymentDueDate;
        private System.Windows.Forms.MonthCalendar calIssueDate;
        private System.Windows.Forms.MonthCalendar calPaymentDueDate;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
    }
}