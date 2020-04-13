using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.WebApi;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAdminServicosPOC.Controllers;
using WebAdminServicosPOC.Servicos;

namespace WebAdminServicosPOC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(x => x.UseSqlServerStorage("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hangfire;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                // sepcify our operation filter here.  
                //c.OperationFilter<AddCommonParameOperationFilter>();

                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = $"v1 API",
                    Description = "v1 API",
                    TermsOfService = new Uri("https://www.c-sharpcorner.com/members/catcher-wong"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Catcher Wong",
                        Email = "catcher_hwq@outlook.com",
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "Apache-2.0",
                        Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                    }
                });
            });

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<Job>().As<IJob>();

            containerBuilder.Populate(services);

            var container = containerBuilder.Build();

            new AutofacServiceProvider(container);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireDashboard();
            //app.UseHangfireServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
