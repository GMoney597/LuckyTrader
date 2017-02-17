namespace LuckyTraderSrv_Form
{
    partial class LT_Svc_Form
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stoppenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostAdresseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.statStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statStripMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerStockUpdater = new System.Windows.Forms.Timer(this.components);
            this.timerMessage = new System.Windows.Forms.Timer(this.components);
            this.rtbInfoScreen = new System.Windows.Forms.RichTextBox();
            this.serverAktivitätenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverToolStripMenuItem,
            this.logFilesToolStripMenuItem,
            this.hilfeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(333, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startenToolStripMenuItem,
            this.stoppenToolStripMenuItem,
            this.toolStripMenuItem1,
            this.beendenToolStripMenuItem});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // startenToolStripMenuItem
            // 
            this.startenToolStripMenuItem.Name = "startenToolStripMenuItem";
            this.startenToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.startenToolStripMenuItem.Text = "Starten";
            this.startenToolStripMenuItem.Click += new System.EventHandler(this.startenToolStripMenuItem_Click);
            // 
            // stoppenToolStripMenuItem
            // 
            this.stoppenToolStripMenuItem.Name = "stoppenToolStripMenuItem";
            this.stoppenToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.stoppenToolStripMenuItem.Text = "Stoppen";
            this.stoppenToolStripMenuItem.Click += new System.EventHandler(this.stoppenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(117, 6);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // logFilesToolStripMenuItem
            // 
            this.logFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverAktivitätenToolStripMenuItem});
            this.logFilesToolStripMenuItem.Name = "logFilesToolStripMenuItem";
            this.logFilesToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.logFilesToolStripMenuItem.Text = "Log-Files";
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hostAdresseToolStripMenuItem,
            this.toolStripMenuItem2,
            this.infoToolStripMenuItem});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hilfeToolStripMenuItem.Text = "Hilfe";
            // 
            // hostAdresseToolStripMenuItem
            // 
            this.hostAdresseToolStripMenuItem.Name = "hostAdresseToolStripMenuItem";
            this.hostAdresseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hostAdresseToolStripMenuItem.Text = "Host-Adresse";
            this.hostAdresseToolStripMenuItem.Click += new System.EventHandler(this.hostAdresseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Wide Latin", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(331, 66);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lucky Trader\r\nServer";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // statStrip
            // 
            this.statStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statStripMessage});
            this.statStrip.Location = new System.Drawing.Point(0, 239);
            this.statStrip.Name = "statStrip";
            this.statStrip.Size = new System.Drawing.Size(333, 22);
            this.statStrip.TabIndex = 2;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel1.Text = "Status: ";
            // 
            // statStripMessage
            // 
            this.statStripMessage.Name = "statStripMessage";
            this.statStripMessage.Size = new System.Drawing.Size(28, 17);
            this.statStripMessage.Text = "###";
            // 
            // timerStockUpdater
            // 
            this.timerStockUpdater.Interval = 300000;
            this.timerStockUpdater.Tick += new System.EventHandler(this.timerStockUpdater_Tick);
            // 
            // timerMessage
            // 
            this.timerMessage.Interval = 3000;
            this.timerMessage.Tick += new System.EventHandler(this.timerMessage_Tick);
            // 
            // rtbInfoScreen
            // 
            this.rtbInfoScreen.Enabled = false;
            this.rtbInfoScreen.Font = new System.Drawing.Font("Helonia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbInfoScreen.Location = new System.Drawing.Point(12, 93);
            this.rtbInfoScreen.Name = "rtbInfoScreen";
            this.rtbInfoScreen.Size = new System.Drawing.Size(309, 143);
            this.rtbInfoScreen.TabIndex = 3;
            this.rtbInfoScreen.Text = "";
            // 
            // serverAktivitätenToolStripMenuItem
            // 
            this.serverAktivitätenToolStripMenuItem.Name = "serverAktivitätenToolStripMenuItem";
            this.serverAktivitätenToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.serverAktivitätenToolStripMenuItem.Text = "Server-Aktivitäten";
            this.serverAktivitätenToolStripMenuItem.Click += new System.EventHandler(this.serverAktivitätenToolStripMenuItem_Click);
            // 
            // LT_Svc_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 261);
            this.Controls.Add(this.rtbInfoScreen);
            this.Controls.Add(this.statStrip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LT_Svc_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lucky Trader Server";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statStrip.ResumeLayout(false);
            this.statStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stoppenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statStripMessage;
        private System.Windows.Forms.Timer timerStockUpdater;
        private System.Windows.Forms.Timer timerMessage;
        private System.Windows.Forms.ToolStripMenuItem hostAdresseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtbInfoScreen;
        private System.Windows.Forms.ToolStripMenuItem serverAktivitätenToolStripMenuItem;
    }
}

