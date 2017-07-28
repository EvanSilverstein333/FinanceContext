using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleInjector;
using Infrastructure.IocInstallers;
using PublisherSubscriberService;
using System.ServiceModel;
using FinanceManager.Contract.Commands;
using SimpleInjector.Integration.Wcf;
using Infrastructure.Abstractions;
using System.ServiceModel.Description;
using Infrastructure.Configurations.WcfServices;
using System.ServiceProcess;
using System.Diagnostics;
using Infrastructure.ErrorHandlers;
using log4net;
using Infrastructure.ServerHosts.WindowsService;

namespace Infrastructure
{
    static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += (sender, e) => FatalExceptionObject.Handle(e.ExceptionObject);
                Application.ThreadException += (sender, e) => FatalExceptionHandler.Handle(e.Exception);
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ApplicationExit += Application_ApplicationExit;

                var container = Bootstrapper.Container;
                ServiceBase[] servicesToRun;
                servicesToRun = new ServiceBase[]
                {
                    new FinanceManagerWindowsService()
                };

                if (Environment.UserInteractive) //debug mode: simulate windows service
                {
                    Application.Run(new FinancialManagerWindowsServiceDebugger(servicesToRun));
                }
                else
                {
                    ServiceBase.Run(servicesToRun);
                }

            }
            catch (Exception e)
            {
                FatalExceptionHandler.Handle(e);
            }



        }



        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (Bootstrapper.Container != null)
            {
                var logger = Bootstrapper.Logger;
                logger.Info("Application session terminated");
                Bootstrapper.Container.Dispose();
            }
        }











    }
}
