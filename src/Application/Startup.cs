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

namespace dotnetcorecrud.Application
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
            IDictionary<string, DbConfiguration> dbConfigurations = BuildDbConfiguration();

            // Add Units of Work
            services.AddSingleton<ITestingUnitOfWork, TestingUnitOfWork>(
                (serviceProvider) => new TestingUnitOfWork(dbConfigurations)
            );
            
            // Add Processors
            services.AddSingleton<IPeopleProcessor, PeopleProcessor>();
        }

        private IDictionary<string, DbConfiguration> BuildDbConfiguration()
        {
            IEnumerable<IConfigurationSection> allConfiguration =
                Configuration.GetChildren().ToList();
            IConfigurationSection dbConfigurationSections = 
                allConfiguration.Where(x => x.Key == "DBConfigurations").FirstOrDefault();
            IDictionary<string, DbConfiguration> dbConfigurations =
                new Dictionary<string, DbConfiguration>();

            foreach (IConfigurationSection section in dbConfigurationSections.GetChildren())
            {
                dbConfigurations.Add(section.Key, new DbConfiguration()
                {
                    ConnectionString = section.GetValue<string>("ConnectionString", ""),
                    ConnectionTimeout = section.GetValue<int>("ConnectionTimeout", 0),
                    BulkInsertTimeout = section.GetValue<int>("BulkInsertTimeout", 0),
                    BulkUpdateTimeout = section.GetValue<int>("BulkUpdateTimeout", 0)
                });
            }

            return dbConfigurations;
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
