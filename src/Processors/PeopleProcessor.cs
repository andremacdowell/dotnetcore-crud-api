using System;
using System.Collections.Generic;
using dotnetcorecrud.Models;
using dotnetcorecrud.Repositories;

namespace dotnetcorecrud.Processors
{
    public class PeopleProcessor : IPeopleProcessor
    {
        private readonly ITestingUnitOfWork _testingUnitOfWork;

        public PeopleProcessor(ITestingUnitOfWork testingUnitOfWork)
        {
            _testingUnitOfWork = testingUnitOfWork;
        }

        public IEnumerable<People> GetAllPeople()
        {
            return _testingUnitOfWork.PeopleRepository.GetAll();
        }

        public People GetPerson(Guid peopleKey)
        {
            return _testingUnitOfWork.PeopleRepository.GetPerson(peopleKey);
        }
    }
}