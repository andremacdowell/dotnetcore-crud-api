using System.Collections.Generic;
using dotnetcorecrud.Commons;

namespace dotnetcorecrud.Repositories 
{
    public class TestingUnitOfWork : ITestingUnitOfWork
    {
        private const string DbName = "TestingDatabase"; 

        private IDictionary<string, DbConfiguration> _dbConfiguration;

        public TestingUnitOfWork()
        {

        }

        public TestingUnitOfWork(IDictionary<string, DbConfiguration> dbConfiguration)
        {
            _dbConfiguration = dbConfiguration;
        }

        private IPeopleRepository _peopleRepository;

        public IPeopleRepository PeopleRepository
        {
            get
            {
                if (_peopleRepository == null)
                {
                    _peopleRepository = new PeopleRepository(_dbConfiguration[DbName]);
                }

                return _peopleRepository;
            }
        }
    }
}