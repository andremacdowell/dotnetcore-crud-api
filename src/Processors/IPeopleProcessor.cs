using System;
using System.Collections.Generic;
using dotnetcorecrud.Models;

namespace dotnetcorecrud.Processors
{
    public interface IPeopleProcessor
    {
        IEnumerable<People> GetAllPeople();

        People GetPerson(Guid key);
    }
}