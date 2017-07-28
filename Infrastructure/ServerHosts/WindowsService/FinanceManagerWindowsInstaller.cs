using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.ServerHosts.WindowsService
{
    [RunInstaller(true)]
    public partial class FinanceManagerWindowsInstaller : System.Configuration.Install.Installer
    {
        public FinanceManagerWindowsInstaller()
        {
            InitializeComponent();
        }
    }
}
