using System;
using System.Collections.Generic;
using System.Linq;
using dotnetcorecrud.DomainModel;
using dotnetcorecrud.DomainModel.DTO;
using dotnetcorecrud.Infrastructure;

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
            IEnumerable<People> result = new List<People>();

            IEnumerable<PeopleQueryResponse> queryResponse =
                _testingUnitOfWork.PeopleRepository.GetAll();
            
            foreach(PeopleQueryResponse peeps in queryResponse)
            {
                result.Append(new People() { Name = peeps.Name, PeopleKey = peeps.PeopleKey } );
            }

            return result;
        }

        public People GetPerson(Guid peopleKey)
        {
            People result = null;
            
            PeopleQueryResponse queryResponse = 
                _testingUnitOfWork.PeopleRepository.GetPerson(peopleKey);
        
            result = new People() { Name = queryResponse.Name, PeopleKey = queryResponse.PeopleKey };

            return result;
        }
    }
}