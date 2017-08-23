using System;
using System.Collections.Generic;
using dotnetcore.Models;

namespace dotnetcore.Processors
{
    public interface IPeopleProcessor
    {
        IEnumerable<People> GetAllPeople();

        People GetPerson(Guid key);
    }
}