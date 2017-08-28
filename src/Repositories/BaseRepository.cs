using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using dotnetcorecrud.Commons;

namespace dotnetcorecrud.Repositories
{
    public class BaseRepository
    {
        private string _connectionString;

        protected SqlConnection _connection;

        public BaseRepository(DbConfiguration dbConfiguration)
        {
            _connectionString = dbConfiguration.ConnectionString;
        }

        protected IDbConnection Connection
        {
            get
            {
                if (_connection == null) 
                {
                    _connection = new SqlConnection(_connectionString);
                }

                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                return _connection;
            }
        }

        protected string GetQueryFromResource(string sqlFileName)
        {
            var resourceStream = 
                Assembly.GetEntryAssembly()
                        .GetManifestResourceStream($"dotnetcore-crud.Repositories.SQL.{sqlFileName}.sql");

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}