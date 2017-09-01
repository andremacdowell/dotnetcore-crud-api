using System;
using System.Collections.Generic;
using dotnetcorecrud.DomainModel.DTO;

namespace dotnetcorecrud.Infrastructure.Repositories 
{
    public interface IPeopleRepository
    {
        IEnumerable<PeopleQueryResponse> GetAll();
        
        PeopleQueryResponse GetPerson(Guid peopleKey);
    }
}