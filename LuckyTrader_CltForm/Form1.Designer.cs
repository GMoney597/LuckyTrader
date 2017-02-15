namespace LuckyTrader_CltForm
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdGetTable = new System.Windows.Forms.Button();
            this.lboxStock = new System.Windows.Forms.ListBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdGetTable
            // 
            this.cmdGetTable.Location = new System.Drawing.Point(12, 12);
            this.cmdGetTable.Name = "cmdGetTable";
            this.cmdGetTable.Size = new System.Drawing.Size(219, 23);
            this.cmdGetTable.TabIndex = 0;
            this.cmdGetTable.Text = "Tabelle holen";
            this.cmdGetTable.UseVisualStyleBackColor = true;
            this.cmdGetTable.Click += new System.EventHandler(this.cmdGetTable_Click);
            // 
            // lboxStock
            // 
            this.lboxStock.FormattingEnabled = true;
            this.lboxStock.Location = new System.Drawing.Point(12, 41);
            this.lboxStock.Name = "lboxStock";
            this.lboxStock.Size = new System.Drawing.Size(421, 173);
            this.lboxStock.TabIndex = 1;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(12, 226);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(219, 23);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Schließen";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 261);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lboxStock);
            this.Controls.Add(this.cmdGetTable);
            this.Name = "Form1";
            this.Text = "Lucky Trader";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdGetTable;
        private System.Windows.Forms.ListBox lboxStock;
        private System.Windows.Forms.Button cmdClose;
    }
}

