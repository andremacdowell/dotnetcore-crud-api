using System.Collections.Generic;
using System.Linq;
using dotnetcorecrud.Infrastructure.Configuration;
using dotnetcorecrud.Infrastructure.Repositories;

namespace dotnetcorecrud.Infrastructure 
{
    public class TestingUnitOfWork : ITestingUnitOfWork
    {
        private string TestingDatabaseName = "TestingDatabase";

        private IDatabaseConfiguration _testingDatabaseConfiguration;

        public TestingUnitOfWork()
        {

        }

        public TestingUnitOfWork(IEnumerable<IDatabaseConfiguration> databaseConfiguration)
        {
            _testingDatabaseConfiguration = 
                databaseConfiguration.Where(x => x.DatabaseName == TestingDatabaseName).FirstOrDefault();
        }

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