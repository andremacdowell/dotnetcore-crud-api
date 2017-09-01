using System.Collections.Generic;
using dotnetcorecrud.Infrastructure.Configuration;

namespace dotnetcorecrud.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        IEnumerable<IDatabaseConfiguration> DatabaseConfigurations { get; }

        IPeopleRepository PeopleRepository { get; }
    }
}