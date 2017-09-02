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
        
        void BatchUpdatePeople(IEnumerable<People> people);
        
        People DeletePerson(Guid peopleKey);
        
        void BatchDeletePeople(IEnumerable<Guid> peopleKeys);

        void BatchCreatePeople(IEnumerable<string> peopleNames);
        
        People CreatePerson(string personName);
    }
}