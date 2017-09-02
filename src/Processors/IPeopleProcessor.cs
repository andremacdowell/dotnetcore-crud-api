using System;
using System.Collections.Generic;
using dotnetcorecrud.DomainModel;

namespace dotnetcorecrud.Processors
{
    public interface IPeopleProcessor
    {
        IEnumerable<People> GetAllPeople();

        People GetPerson(Guid key);

        People UpdatePerson(People person);
        
        Guid BatchUpdatePeople(IEnumerable<People> people);
        
        People DeletePerson(Guid peopleKey);
        
        Guid BatchDeletePeople(IEnumerable<Guid> peopleKeys);

        Guid BatchCreatePeople(IEnumerable<string> peopleNames);
        
        People CreatePerson(string personName);
    }
}