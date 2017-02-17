using LuckyTraderLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LuckyTraderSrv_Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri serviceAddress;
            serviceAddress = GetServiceEndpoints();
            ServiceHost svcHost = new ServiceHost(typeof(LuckyTraderService), serviceAddress);

            svcHost.Open();
            Console.WriteLine("LuckyTrader-Service online");
            Console.WriteLine("==========================");
            Console.WriteLine("Service-Baseaddress: " + serviceAddress);

            Stopwatch timer = new Stopwatch();
            TimeSpan tSpan = new TimeSpan(0, 5, 0);
            timer.Start();

            while (svcHost.State == CommunicationState.Opened)
            {
                if (timer.Elapsed == tSpan)
                {
                    LuckyTraderService lts = new LuckyTraderService();
                    lts.AutoUpdateStock();
                    timer.Restart();
                }
            }

            // Durch die Dauer-Prüf-Schleife von "while" wird dieser Teil des Codes nie erreicht
            Console.WriteLine("\nDrücken Sie <ENTER> um den Service zu stoppen");
            Console.ReadLine();
        }

        private static Uri GetServiceEndpoints()
        {
            Uri localhost = new Uri("http://localhost/");

            if (Dns.GetHostName() == "Gamer")
            {
                Uri hostGamer = new Uri("http://gamer/");
                return hostGamer;
            }
            else if (Dns.GetHostName() == "Developer")
            {
                Uri hostDeveloper = new Uri("http://developer/");
                return hostDeveloper;
            }
            else if (Dns.GetHostName() == "DT-Sarnoch1")
            {
                Uri hostOffice = new Uri("http://dt-sarnoch1/");
                return hostOffice;
            }
            else
                return localhost;
        }
    }
}
