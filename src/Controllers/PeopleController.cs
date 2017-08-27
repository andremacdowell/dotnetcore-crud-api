using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcorecrud.Models;
using dotnetcorecrud.Processors;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcorecrud.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly IPeopleProcessor _peopleProcessor;

        public PeopleController(IPeopleProcessor peopleProcessor)
        {
            _peopleProcessor = peopleProcessor;
        }

        // GET api/people
        [HttpGet("all")]
        public IEnumerable<People> GetAll()
        {
            IEnumerable<People> result = _peopleProcessor.GetAllPeople();
            
            return result;
        }

        // GET api/people/<peopleKey>
        [HttpGet("{peopleKey}")]
        public People GetPerson(string peopleKey)
        {
            People result = _peopleProcessor.GetPerson(Guid.Parse(peopleKey));

            return result;
        }

        // PUT api/people
        [HttpPut]
        public People Put([FromBody]People person)
        {
            throw new NotImplementedException();
        }

        // PUT api/people/batch
        [HttpPut("batch")]
        public void BatchPut(Guid peopleKey)
        {
            throw new NotImplementedException();
        }

        // DELETE api/people/<peopleKey>
        [HttpDelete("{key}")]
        public void Delete(Guid peopleKey)
        {
            throw new NotImplementedException();
        }

        // DELETE api/people/<peopleKey>
        [HttpDelete("batch")]
        public void BatchDelete(Guid peopleKey)
        {
            throw new NotImplementedException();
        }
    }
}
