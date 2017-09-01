using System.Collections.Generic;

namespace dotnetcorecrud.Infrastructure.Configuration
{
    public interface IDatabaseConfiguration
    {
        string DatabaseName { get; set; }

        string ConnectionString { get; set; }

        int ConnectionTimeout  { get; set; }
        
        int BulkInsertTimeout  { get; set; }
        
        int UpdateTimeout  { get; set; }    
    }
}