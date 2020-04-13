using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAdminServicosPOC.WindowsService
{
    public class Service
    {
        private BackgroundJobServer _server;

        public void Start()
        {
            _server = new BackgroundJobServer();
        }

        public void Stop()
        {
            _server.Dispose();
        }
    }
}
