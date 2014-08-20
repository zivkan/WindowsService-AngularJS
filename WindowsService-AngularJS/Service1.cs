using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Service.Web;

namespace WindowsService_AngularJS
{
    public partial class Service1 : ServiceBase
    {
        private IDisposable web;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            web = OwinStartup.Start();
        }

        protected override void OnStop()
        {
            if (web != null)
            {
                web.Dispose();
                web = null;
            }
        }
    }
}
