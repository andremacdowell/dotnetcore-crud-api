namespace dotnetcorecrud.Infrastructure.Configuration
{
    public class DatabaseConfiguration
    {
        public string ConnectionString;

        public int ConnectionTimeout;
        
        public int BulkInsertTimeout;
        
        public int BulkUpdateTimeout;
    }
}