using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using dotnetcorecrud.Infrastructure.Configuration;
using dotnetcorecrud.Infrastructure;
using dotnetcorecrud.DomainModel.DTO;

namespace dotnetcorecrud.Infrastructure.Repositories 
{
    public class PeopleRepository : BaseRepository, IPeopleRepository
    {
        public PeopleRepository(IDatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
        }

        public IEnumerable<PeopleQueryResponse> GetAll()
        {
            string query = GetQueryFromResource("GetAllPeople");
            return Connection.Query<PeopleQueryResponse>(query);
        }

        public PeopleQueryResponse GetPerson(Guid peopleKey)
        {
            string query = GetQueryFromResource("GetPeopleByKey");
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("peopleKey", peopleKey, DbType.Guid);

            return Connection.Query<PeopleQueryResponse>(query, parameters).ToList().FirstOrDefault();
        }
    }
}