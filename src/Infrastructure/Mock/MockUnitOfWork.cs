using System.Collections.Generic;
using dotnetcorecrud.Infrastructure.Configuration;
using dotnetcorecrud.Infrastructure.Repositories;

namespace dotnetcorecrud.Infrastructure.Mock
{
    public class MockUnitOfWork : IUnitOfWork
    {
        private IEnumerable<IDatabaseConfiguration> _databaseConfigurations;

        public MockUnitOfWork(IEnumerable<IDatabaseConfiguration> databaseConfigurations)
        {
            _databaseConfigurations = databaseConfigurations;
        }

        public IPeopleRepository PeopleRepository => new MockPeopleRepository();

        IEnumerable<IDatabaseConfiguration> IUnitOfWork.DatabaseConfigurations => _databaseConfigurations;
    }
}