using System;
using System.Collections.Generic;
using dotnetcorecrud.DomainModel;

namespace dotnetcorecrud.Processors
{
    public interface IPeopleProcessor
    {
        IEnumerable<People> GetAllPeople();

        People GetPerson(Guid key);
    }
}