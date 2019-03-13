namespace BillTracker
{
    partial class AddIssuerScreen
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
            this.btnAddIssuer = new System.Windows.Forms.Button();
            this.tbAddIssuer = new System.Windows.Forms.TextBox();
            this.btnCancelAddingIssuer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddIssuer
            // 
            this.btnAddIssuer.Location = new System.Drawing.Point(175, 65);
            this.btnAddIssuer.Name = "btnAddIssuer";
            this.btnAddIssuer.Size = new System.Drawing.Size(80, 23);
            this.btnAddIssuer.TabIndex = 0;
            this.btnAddIssuer.Text = "Dodaj";
            this.btnAddIssuer.UseVisualStyleBackColor = true;
            this.btnAddIssuer.Click += new System.EventHandler(this.btnAddIssuer_Click);
            // 
            // tbAddIssuer
            // 
            this.tbAddIssuer.Location = new System.Drawing.Point(50, 30);
            this.tbAddIssuer.Name = "tbAddIssuer";
            this.tbAddIssuer.Size = new System.Drawing.Size(230, 20);
            this.tbAddIssuer.TabIndex = 1;
            // 
            // btnCancelAddingIssuer
            // 
            this.btnCancelAddingIssuer.Location = new System.Drawing.Point(80, 65);
            this.btnCancelAddingIssuer.Name = "btnCancelAddingIssuer";
            this.btnCancelAddingIssuer.Size = new System.Drawing.Size(75, 23);
            this.btnCancelAddingIssuer.TabIndex = 2;
            this.btnCancelAddingIssuer.Text = "Anuluj";
            this.btnCancelAddingIssuer.UseVisualStyleBackColor = true;
            this.btnCancelAddingIssuer.Click += new System.EventHandler(this.btnCancelAddingIssuer_Click);
            // 
            // AddIssuerScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 106);
            this.Controls.Add(this.btnCancelAddingIssuer);
            this.Controls.Add(this.tbAddIssuer);
            this.Controls.Add(this.btnAddIssuer);
            this.Name = "AddIssuerScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dodaj wystawiającego";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddIssuer;
        public System.Windows.Forms.TextBox tbAddIssuer;
        private System.Windows.Forms.Button btnCancelAddingIssuer;
    }
}