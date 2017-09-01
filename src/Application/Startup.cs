using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcorecrud.Infrastructure.Configuration;
using dotnetcorecrud.Processors;
using dotnetcorecrud.Infrastructure;
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
            IDictionary<string, DatabaseConfiguration> databaseConfigurations = BuildDbConfiguration();

            // Add Units of Work
            services.AddSingleton<ITestingUnitOfWork, TestingUnitOfWork>(
                (serviceProvider) => new TestingUnitOfWork(databaseConfigurations)
            );
            
            // Add Processors
            services.AddSingleton<IPeopleProcessor, PeopleProcessor>();
        }

        private IDictionary<string, DatabaseConfiguration> BuildDbConfiguration()
        {
            IEnumerable<IConfigurationSection> allConfiguration =
                Configuration.GetChildren().ToList();
            IConfigurationSection dbConfigurationSections = 
                allConfiguration.Where(x => x.Key == "DatabaseConfigurations").FirstOrDefault();
            IDictionary<string, DatabaseConfiguration> databaseConfigurations =
                new Dictionary<string, DatabaseConfiguration>();

            foreach (IConfigurationSection section in dbConfigurationSections.GetChildren())
            {
                databaseConfigurations.Add(section.Key, new DatabaseConfiguration()
                {
                    ConnectionString = section.GetValue<string>("ConnectionString", ""),
                    ConnectionTimeout = section.GetValue<int>("ConnectionTimeout", 0),
                    BulkInsertTimeout = section.GetValue<int>("BulkInsertTimeout", 0),
                    BulkUpdateTimeout = section.GetValue<int>("BulkUpdateTimeout", 0)
                });
            }

            return databaseConfigurations;
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
