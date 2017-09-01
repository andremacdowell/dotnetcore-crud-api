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
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IEnumerable<IDatabaseConfiguration> databaseConfigurations = new List<DatabaseConfiguration>();
            foreach(IConfigurationSection configurationSection in Configuration.GetSection("DatabaseConfigurations").GetChildren())
            {
                IDatabaseConfiguration test = new DatabaseConfiguration().Map(configurationSection);
                databaseConfigurations = databaseConfigurations.Append(test);
            }
            
            // Add Units of Work
            services.AddSingleton<ITestingUnitOfWork, TestingUnitOfWork>(
                (serviceProvider) => new TestingUnitOfWork(databaseConfigurations)
            );
            
            // Add Processors
            services.AddSingleton<IPeopleProcessor, PeopleProcessor>();

            services.AddMvc();
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
