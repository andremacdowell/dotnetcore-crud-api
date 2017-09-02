using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcorecrud.Infrastructure.Configuration;
using dotnetcorecrud.Processors;
using dotnetcorecrud.Infrastructure.Repositories;
using dotnetcorecrud.Infrastructure.Dapper;
using dotnetcorecrud.Infrastructure.Mock;
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
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

            // Sets up Configuration Handling
            var builder = new ConfigurationBuilder()
                .SetBasePath(_hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_hostingEnvironment.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Sets up configuration
            IEnumerable<IDatabaseConfiguration> databaseConfigurations = new List<DatabaseConfiguration>();
            foreach(IConfigurationSection configurationSection in Configuration.GetSection("DatabaseConfigurations").GetChildren())
            {
                IDatabaseConfiguration test = new DatabaseConfiguration().Map(configurationSection);
                databaseConfigurations = databaseConfigurations.Append(test);
            }
            
            // Adds Unit of Work
            if (_hostingEnvironment.IsDevelopment())
            {
                services.AddSingleton<IUnitOfWork, MockUnitOfWork>(
                    (serviceProvider) => new MockUnitOfWork(databaseConfigurations)
                );
            }
            else
            {
                services.AddSingleton<IUnitOfWork, UnitOfWork>(
                    (serviceProvider) => new UnitOfWork(databaseConfigurations)
                );
            }
            
            // Adds Processors
            services.AddSingleton<IPeopleProcessor, PeopleProcessor>();
        
            // Adds Memory Cache
            services.AddMemoryCache();
         
            // Adds Mvc
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }
    }
}
