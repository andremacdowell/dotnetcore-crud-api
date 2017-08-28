using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using dotnetcorecrud.Commons;
using dotnetcorecrud.Models;

namespace dotnetcorecrud.Repositories 
{
    public class PeopleRepository : BaseRepository, IPeopleRepository
    {
        public PeopleRepository(DbConfiguration dbConfiguration) : base(dbConfiguration)
        {
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
    }
}