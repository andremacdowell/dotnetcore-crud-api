using System.Collections.Generic;

namespace dotnetcorecrud.Infrastructure.Configuration
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public DatabaseConfiguration()
        {
        }

        public string DatabaseName { get; set; }

        public string ConnectionString { get; set; }

        public int ConnectionTimeout  { get; set; }
        
        public int BulkInsertTimeout  { get; set; }
        
        public int UpdateTimeout  { get; set; } 
    }
}