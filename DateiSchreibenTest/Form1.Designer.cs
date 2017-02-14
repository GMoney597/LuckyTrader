namespace DateiSchreibenTest
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
            this.txtErrorLog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCreateErrorLogFile = new System.Windows.Forms.Button();
            this.dateLogFile = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // txtErrorLog
            // 
            this.txtErrorLog.Location = new System.Drawing.Point(12, 25);
            this.txtErrorLog.MaxLength = 250;
            this.txtErrorLog.Multiline = true;
            this.txtErrorLog.Name = "txtErrorLog";
            this.txtErrorLog.Size = new System.Drawing.Size(260, 54);
            this.txtErrorLog.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Diesen Text in eine Datei schreiben:";
            // 
            // cmdCreateErrorLogFile
            // 
            this.cmdCreateErrorLogFile.Location = new System.Drawing.Point(15, 85);
            this.cmdCreateErrorLogFile.Name = "cmdCreateErrorLogFile";
            this.cmdCreateErrorLogFile.Size = new System.Drawing.Size(75, 39);
            this.cmdCreateErrorLogFile.TabIndex = 2;
            this.cmdCreateErrorLogFile.Text = "Text schreiben";
            this.cmdCreateErrorLogFile.UseVisualStyleBackColor = true;
            this.cmdCreateErrorLogFile.Click += new System.EventHandler(this.cmdCreateErrorLogFile_Click);
            // 
            // dateLogFile
            // 
            this.dateLogFile.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateLogFile.Location = new System.Drawing.Point(172, 85);
            this.dateLogFile.Name = "dateLogFile";
            this.dateLogFile.Size = new System.Drawing.Size(100, 20);
            this.dateLogFile.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 131);
            this.Controls.Add(this.dateLogFile);
            this.Controls.Add(this.cmdCreateErrorLogFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtErrorLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtErrorLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdCreateErrorLogFile;
        private System.Windows.Forms.DateTimePicker dateLogFile;
    }
}

