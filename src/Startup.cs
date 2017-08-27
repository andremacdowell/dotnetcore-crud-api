using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcorecrud.Commons;
using dotnetcorecrud.Processors;
using dotnetcorecrud.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace dotnetcorecrud
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
            services.AddMvc();

            IntegrateDependencyInjector(services);
        }

        private void IntegrateDependencyInjector(IServiceCollection services)
        {
            Dictionary<string, DbConfiguration> dbConfiguration = BuildDbConfiguration();

            // Add Units of Work
            services.AddSingleton<ITestingUnitOfWork, TestingUnitOfWork>((serviceProvider) => new TestingUnitOfWork(dbConfiguration));
            
            // Add Processors
            services.AddSingleton<IPeopleProcessor, PeopleProcessor>();
        }

        private Dictionary<string, DbConfiguration> BuildDbConfiguration()
        {
            string[] dbs = { "TestingDatabase" };
            var allConfiguration = Configuration.GetChildren().ToList();
            
            IConfigurationSection dbConfigurationSections = (
                from config in allConfiguration where config.Key == "DBConfigurations" select config
            ).FirstOrDefault();
            
            var dbConfiguration = new Dictionary<string, DbConfiguration>();
            foreach (string db in dbs)
            {
                dbConfiguration.Add("TestingDatabase", new DbConfiguration()
                {
                    ConnectionString = dbConfigurationSections.GetSection(db).GetSection("ConnectionString").Value,
                    ConnectionTimeout = Int32.Parse(dbConfigurationSections.GetSection(db).GetValue("ConnectionTimeout", "0")),
                    BulkInsertTimeout = Int32.Parse(dbConfigurationSections.GetSection(db).GetValue("BulkInsertTimeout", "0")),
                    BulkUpdateTimeout = Int32.Parse(dbConfigurationSections.GetSection(db).GetValue("BulkUpdateTimeout", "0"))
                });
            }

            return dbConfiguration;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
