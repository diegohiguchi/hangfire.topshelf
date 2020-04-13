using Hangfire;
using Serilog;
using System;
using Topshelf;

namespace WebAdminServicosPOC.WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            //Serilog Configuration: Log to console and file
            Log.Logger = new LoggerConfiguration()
            .WriteTo.ColoredConsole()
            .WriteTo.RollingFile(@"C:\Serilogs\RouteDelivery-Service-{Date}.txt")
            .CreateLogger();

            //Hangfire Job Storage 
            GlobalConfiguration.Configuration.UseSqlServerStorage("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hangfire;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            //Topshelf
            HostFactory.Run(x =>
            {
                x.UseSerilog();

                x.Service<Service>(s =>
                {
                    s.ConstructUsing(name => new Service());
                    s.WhenStarted(rs => rs.Start());
                    s.WhenStopped(rs => rs.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Service");
                x.SetDisplayName("Service");
                x.SetServiceName("Service");
            });
        }
    }
}
