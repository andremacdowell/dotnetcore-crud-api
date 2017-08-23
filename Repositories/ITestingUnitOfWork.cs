namespace dotnetcore.Repositories 
{
    public interface ITestingUnitOfWork
    {
        IPeopleRepository PeopleRepository { get; }
    }
}