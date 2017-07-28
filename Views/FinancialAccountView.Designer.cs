namespace Views
{
    partial class FinancialAccountView
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.AccountsGrid = new System.Windows.Forms.DataGridView();
            this.btnAddAccount = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AccountsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnAddAccount);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.AccountsGrid);
            this.splitContainer1.Size = new System.Drawing.Size(543, 336);
            this.splitContainer1.SplitterDistance = 181;
            this.splitContainer1.TabIndex = 0;
            // 
            // AccountsGrid
            // 
            this.AccountsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AccountsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AccountsGrid.Location = new System.Drawing.Point(0, 0);
            this.AccountsGrid.Name = "AccountsGrid";
            this.AccountsGrid.RowTemplate.Height = 24;
            this.AccountsGrid.Size = new System.Drawing.Size(358, 336);
            this.AccountsGrid.TabIndex = 1;
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddAccount.Location = new System.Drawing.Point(0, 0);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(181, 23);
            this.btnAddAccount.TabIndex = 0;
            this.btnAddAccount.Text = "Add Account";
            this.btnAddAccount.UseVisualStyleBackColor = true;
            // 
            // FinancialAccountView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 336);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FinancialAccountView";
            this.Text = "FinancialAccountView";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AccountsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnAddAccount;
        private System.Windows.Forms.DataGridView AccountsGrid;
    }
}