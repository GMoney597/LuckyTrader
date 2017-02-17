using LuckyTraderLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace LuckyTraderWinSvc
{
    public partial class LTService : ServiceBase
    {
        private ServiceHost svcHost;
        private Uri serviceAddress;

        public LTService()
        {
            InitializeComponent();
            serviceAddress = GetServiceEndpoints();
            ServiceName = "LuckyTRaderWinServiceHost";
        }

        protected override void OnStart(string[] args)
        {
            svcHost = new ServiceHost(typeof(LuckyTraderService), serviceAddress);
            svcHost.Open();

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
        }

        protected override void OnStop()
        {
            if (svcHost != null)
            {
                svcHost.Close();
                svcHost = null;
            }
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