namespace dotnetcorecrud.Repositories 
{
    public interface ITestingUnitOfWork
    {
        IPeopleRepository PeopleRepository { get; }
    }
}