using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using dotnetcore.Commons;
using dotnetcore.Models;

namespace dotnetcore.Repositories 
{
    public class PeopleRepository : IPeopleRepository
    {
        private string _connectionString;

        private SqlConnection _connection;
    
        public PeopleRepository(DbConfiguration dbConfiguration)
        {
            _connectionString = dbConfiguration.ConnectionString;
        }

        private IDbConnection Connection
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

        public IEnumerable<People> GetAll()
        {
            string query = GetQueryFromResource("GetAllPeople");
            return Connection.Query<People>(query);
        }

        public People GetPerson(Guid peopleKey)
        {
            string query = GetQueryFromResource("GetPeopleByKey");
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("peopleKey", peopleKey, DbType.Guid);

            return Connection.Query<People>(query, parameters).ToList().FirstOrDefault();
        }

        private string GetQueryFromResource(string sqlFileName)
        {
            var resourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream($"dotnetcore.Repositories.SQL.{sqlFileName}.sql");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}