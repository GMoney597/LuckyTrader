using System;
using System.IO;
using System.Net;

namespace LuckyTraderLib
{
    internal class LoginMessage
    {
        private FileStream fs;
        private StreamWriter strWri;

        public LoginMessage()
        {
        }

        internal void WriteLogin(string responseCode, string responseMessage)
        {
            DateTime errorAppear = DateTime.Now;
            string hostName = Dns.GetHostName();

            string fileName = string.Format("Logins_{0:yyyy-MM-dd}.log", errorAppear.ToShortDateString());

            if (File.Exists(fileName))
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                fs.Close();
            }
            else
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                fs.Close();
                InitializeLoginLogFile(fileName);
            }

            strWri = new StreamWriter(fileName, true);
            strWri.WriteLine("{0,-7} {1,-15} {2,-8} {3,-80}", errorAppear.ToShortTimeString(), hostName, "#L" + responseCode, responseMessage);
            strWri.Close();
            fs.Close();
        }

        private void InitializeLoginLogFile(string fileName)
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