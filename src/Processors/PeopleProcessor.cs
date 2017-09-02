using System;
using System.Collections.Generic;
using System.Linq;
using dotnetcorecrud.DomainModel;
using dotnetcorecrud.DomainModel.DTO;
using dotnetcorecrud.Infrastructure.Repositories;

namespace dotnetcorecrud.Processors
{
    public class PeopleProcessor : IPeopleProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public PeopleProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void BatchCreatePeople(IEnumerable<string> peopleNames)
        {
            throw new NotImplementedException();
        }

        public void BatchDeletePeople(IEnumerable<Guid> peopleKeys)
        {
            throw new NotImplementedException();
        }

        public void BatchUpdatePeople(IEnumerable<People> people)
        {
            throw new NotImplementedException();
        }

        public People CreatePerson(string personName)
        {
            throw new NotImplementedException();
        }

        public People UpdatePerson(People person)
        {
            throw new NotImplementedException();
        }

        public People DeletePerson(Guid peopleKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<People> GetAllPeople()
        {
            IEnumerable<People> result = new List<People>();

            IEnumerable<PeopleQueryResponse> queryResponse =
                _unitOfWork.PeopleRepository.GetAll();
            
            foreach(PeopleQueryResponse person in queryResponse)
            {
                result = result.Append(new People() { Name = person.Name, PeopleKey = person.PeopleKey } );
            }

            return result;
        }

        public People GetPerson(Guid peopleKey)
        {
            People result = null;
            
            PeopleQueryResponse queryResponse = 
                _unitOfWork.PeopleRepository.GetPerson(peopleKey);
        
            result = new People() { Name = queryResponse.Name, PeopleKey = queryResponse.PeopleKey };

            return result;
        }
    }
}