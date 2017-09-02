using System;
using System.Collections.Generic;
using dotnetcorecrud.DomainModel.DTO;
using dotnetcorecrud.Infrastructure.Repositories;

namespace dotnetcorecrud.Infrastructure.Mock
{
    public class MockPeopleRepository : IPeopleRepository
    {
        public MockPeopleRepository()
        {
        }

        public IEnumerable<PeopleQueryResponse> GetAll()
        {
            var mockPerson = new PeopleQueryResponse() { Id = 1, Name = "Helio", PeopleKey = Guid.NewGuid() };
            return new List<PeopleQueryResponse>() { mockPerson };
        }

        public PeopleQueryResponse GetPerson(Guid peopleKey)
        {
            return new PeopleQueryResponse() { Id = 1, Name = "Helio", PeopleKey = Guid.NewGuid() };
        }
    }
}