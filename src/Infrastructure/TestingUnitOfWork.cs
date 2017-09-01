using System.Collections.Generic;
using dotnetcorecrud.Infrastructure.Configuration;
using dotnetcorecrud.Infrastructure.Repositories;

namespace dotnetcorecrud.Infrastructure 
{
    public class TestingUnitOfWork : ITestingUnitOfWork
    {
        private const string DbName = "TestingDatabase"; 

        private IDictionary<string, DatabaseConfiguration> _databaseConfiguration;

        public TestingUnitOfWork()
        {

        }

        public TestingUnitOfWork(IDictionary<string, DatabaseConfiguration> databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        private IPeopleRepository _peopleRepository;

        public IPeopleRepository PeopleRepository
        {
            get
            {
                if (_peopleRepository == null)
                {
                    _peopleRepository = new PeopleRepository(_databaseConfiguration[DbName]);
                }

                return _peopleRepository;
            }
        }
    }
}