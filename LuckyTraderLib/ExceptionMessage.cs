using System;
using System.IO;
using System.Net;

namespace LuckyTraderLib
{
    internal class ExceptionMessage
    {
        private FileStream fs;
        private StreamWriter strWri;

        internal void DoWriteExceptionLog(Exception ex)
        {
            DateTime exceptionAppear = DateTime.Now;
            string hostName = Dns.GetHostName();

            string fileName = string.Format("Exception_{0:yyyy-MM-dd}.log", exceptionAppear.ToShortDateString());

            if (File.Exists(fileName))
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                fs.Close();
            }
            else
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                fs.Close();
                InitializeExceptionLogFile(fileName);
            }

            strWri = new StreamWriter(fileName, true);
            strWri.WriteLine("{0,-7} {1,-15} {2,-16} {3,-80}", exceptionAppear.ToShortTimeString(), hostName, "#L" + ex.HResult, ex.Message);
            strWri.Close();
            fs.Close();
        }

        private void InitializeExceptionLogFile(string fileName)
        {
            strWri = new StreamWriter(fileName, true);
            strWri.WriteLine("{0,-7} {1,-15} {2,-16} {3,-80}", "DATUM:", "Client:", "Code:", "Meldung:");
            string dummy = "=";
            strWri.WriteLine(dummy.PadLeft(109, '='));
            strWri.Close();
            fs.Close();
        }

    }
}