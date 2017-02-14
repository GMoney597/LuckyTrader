using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace DateiSchreibenTest
{
    public partial class Form1 : Form
    {
        FileStream fs;
        StreamWriter strWri;

        public Form1()
        {
            InitializeComponent();
        }

        private void cmdCreateErrorLogFile_Click(object sender, EventArgs e)
        {
            DateTime errorAppear = DateTime.Now;
            string hostName = Dns.GetHostName();

            string fileName = string.Format("Error_{0:yyyy-MM-dd}.log", dateLogFile.Value);

            if (File.Exists(fileName))
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                fs.Close();
            }
            else
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                fs.Close();
                InitializeErrorLogFile(fileName);
            }

            strWri = new StreamWriter(fileName, true);
            strWri.WriteLine(errorAppear.ToShortTimeString() + ":\t" + hostName + "\t#C0001\t" + txtErrorLog.Text);
            strWri.Close();
            fs.Close();

            txtErrorLog.Clear();
        }

        private void InitializeErrorLogFile(string fileName)
        {
            strWri = new StreamWriter(fileName, true);
            strWri.WriteLine("DATUM:\tClient\t\tCode\tMeldung");
            strWri.WriteLine("=======================================================================================");
            strWri.Close();
            fs.Close();
        }
    }
}
