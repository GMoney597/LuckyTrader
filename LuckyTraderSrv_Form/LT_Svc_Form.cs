using LuckyTraderLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckyTraderSrv_Form
{
    public partial class LT_Svc_Form : Form
    {
        Uri serviceAddress;
        ServiceHost svcHost;

        public LT_Svc_Form()
        {
            InitializeComponent();
            statStripMessage.Text = "";
            ActivateStockUpdater();
        }

        private void ActivateStockUpdater()
        {
            timerStockUpdater.Start();
        }

        private Uri GetServiceAddress()
        {
            string hostName = Dns.GetHostName();

            if (hostName.ToLower() == "gamer")
                return new Uri("http://gamer/");
            else if (hostName.ToLower() == "developer")
                return new Uri("http://developer/");
            else if (hostName.ToLower() == "dt-sarnoch1")
                return new Uri("http://dt-sarnoch1");
            else
                return new Uri("http://localhost/");
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stoppenToolStripMenuItem_Click(this, e);
            Application.Exit();
        }

        private void startenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (svcHost == null)
            {
                serviceAddress = GetServiceAddress();
                svcHost = new ServiceHost(typeof(LuckyTraderService), serviceAddress);
                svcHost.Open();
                statStripMessage.Text = "Service running";
                timerMessage.Start();
            }
            else
            {
                svcHost.Open();
                statStripMessage.Text = "Service running";
                timerMessage.Start();
            }
        }

        private void stoppenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (svcHost != null)
            {
                svcHost.Close();
                svcHost = null;
                statStripMessage.Text = "Service stopped";
                timerMessage.Start();
            }
            else
            {
                statStripMessage.Text = "No active service";
                timerMessage.Start();
            }
        }

        private void timerStockUpdater_Tick(object sender, EventArgs e)
        {
            LuckyTraderService lst = new LuckyTraderService();
            lst.AutoUpdateStock();
            statStripMessage.Text = "Stock Updated";
            timerMessage.Start();
        }

        private void timerMessage_Tick(object sender, EventArgs e)
        {
            timerMessage.Stop();
            statStripMessage.Text = "";
        }

        private void hostAdresseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbInfoScreen.Text = "Dieser Service ist erreichbar unter:\n" + serviceAddress;
        }

        private void serverAktivitätenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbInfoScreen.Text = "Diese Funktion steht noch nicht zur Verfügung";
            //Process.Start("Editor.exe");
        }
    }
}
