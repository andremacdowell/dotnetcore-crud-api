using System;
using System.Collections.Generic;
using dotnetcore.Models;

namespace dotnetcore.Repositories 
{
    public interface IPeopleRepository
    {
        IEnumerable<People> GetAll();
        
        People GetPerson(Guid peopleKey);
    }
}