using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace LuckyTraderWinSvc
{
    [RunInstaller(true)]
    public partial class LT_SvcHost_Installer : Installer
    {
        public LT_SvcHost_Installer()
        {
            InitializeComponent();

            ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
            ServiceInstaller svcInstaller = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.NetworkService;
            svcInstaller.StartType = ServiceStartMode.Automatic;
            svcInstaller.DisplayName = "LT_Service_Host";
            svcInstaller.ServiceName = "LT_Service_Host_Win";

            Installers.Add(processInstaller);
            Installers.Add(svcInstaller);
        }
    }
}
