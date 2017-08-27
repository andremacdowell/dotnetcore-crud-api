using System;
using System.Collections.Generic;
using dotnetcorecrud.Models;

namespace dotnetcorecrud.Repositories 
{
    public interface IPeopleRepository
    {
        IEnumerable<People> GetAll();
        
        People GetPerson(Guid peopleKey);
    }
}