using System.Collections.Generic;
using System.Linq;
using dotnetcorecrud.Infrastructure.Configuration;
using dotnetcorecrud.Infrastructure.Repositories;

namespace dotnetcorecrud.Infrastructure.Dapper
{
    public class UnitOfWork : IUnitOfWork
    {
        private string TestingDatabaseName = "TestingDatabase";

        private IDatabaseConfiguration _testingDatabaseConfiguration;

        private IEnumerable<IDatabaseConfiguration> _databaseConfigurations;

        public UnitOfWork(IEnumerable<IDatabaseConfiguration> databaseConfigurations)
        {
            _databaseConfigurations = databaseConfigurations;
            _testingDatabaseConfiguration = 
                _databaseConfigurations.Where(x => x.DatabaseName == TestingDatabaseName).FirstOrDefault();
        }

        public IEnumerable<IDatabaseConfiguration> DatabaseConfigurations => _databaseConfigurations;

        private IPeopleRepository _peopleRepository;

        public IPeopleRepository PeopleRepository
        {
            get
            {
                if (_peopleRepository == null)
                {
                    _peopleRepository = new PeopleRepository(_testingDatabaseConfiguration);
                }

                return _peopleRepository;
            }
        }
    }
}