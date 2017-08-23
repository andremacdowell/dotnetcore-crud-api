using System.Collections.Generic;
using dotnetcore.Commons;

namespace dotnetcore.Repositories 
{
    public class TestingUnitOfWork : ITestingUnitOfWork
    {
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
                    _peopleRepository = new PeopleRepository(_dbConfiguration["TestingDatabase"]);
                }

                return _peopleRepository;
            }
        }
    }
}