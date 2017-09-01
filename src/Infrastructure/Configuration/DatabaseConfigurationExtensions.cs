using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace dotnetcorecrud.Infrastructure.Configuration
{
    public static class DatabaseConfigurationExtensions
    {
        public static IDatabaseConfiguration Map(this IDatabaseConfiguration databaseConfiguration, IConfigurationSection configurationSection)
        {
            databaseConfiguration.DatabaseName = configurationSection.Key;
            databaseConfiguration.ConnectionString = configurationSection.GetValue("ConnectionString", "");
            databaseConfiguration.ConnectionTimeout = configurationSection.GetValue("ConnectionTimeout", 0);
            databaseConfiguration.BulkInsertTimeout = configurationSection.GetValue("BulkInsertTimeout", 0);
            databaseConfiguration.UpdateTimeout = configurationSection.GetValue("UpdateTimeout", 0);

            return databaseConfiguration;
        }
    }
}