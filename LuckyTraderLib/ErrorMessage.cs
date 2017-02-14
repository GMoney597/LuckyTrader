using System;
using System.IO;
using System.Net;

namespace LuckyTraderLib
{
    internal class ErrorMessage
    {
        private FileStream fs;
        private StreamWriter strWri;

        public ErrorMessage() { }

        public void WriteErrorLog(string errNumber, string errMessage)
        {
            DateTime errorAppear = DateTime.Now;
            string hostName = Dns.GetHostName();

            string fileName = string.Format("Error_{0:yyyy-MM-dd}.log", errorAppear.ToShortDateString());

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
            strWri.WriteLine("{0,-7} {1,-15} {2,-8} {3,-80}", errorAppear.ToShortTimeString(), hostName, "#L" + errNumber, errMessage);
            strWri.Close();
            fs.Close();
        }

        private void InitializeErrorLogFile(string fileName)
        {
            strWri = new StreamWriter(fileName, true);
            strWri.WriteLine("{0,-7} {1,-15} {2,-8} {3,-80}", "DATUM:", "Client:", "Code:", "Meldung:");
            string dummy = "=";
            strWri.WriteLine(dummy.PadLeft(109, '='));
            strWri.Close();
            fs.Close();
        }
    }
}