using dotnetcorecrud.Infrastructure.Repositories;

namespace dotnetcorecrud.Infrastructure 
{
    public interface ITestingUnitOfWork
    {
        IPeopleRepository PeopleRepository { get; }
    }
}