using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcorecrud.DomainModel;
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

        // GET api/people/all
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

        // POST api/people
        [HttpPost]
        public IActionResult Post([FromBody]string personName)
        {
            People result = _peopleProcessor.CreatePerson(personName);

            return Created("api/people", result);
        }

        // POST api/people/batch
        [HttpPost("batch")]
        public IActionResult BatchPost([FromBody]IEnumerable<string> peopleNames)
        {
            _peopleProcessor.BatchCreatePeople(peopleNames);

            return Accepted();
        }

        // PUT api/people
        [HttpPut]
        public People Put([FromBody]People person)
        {
            People result = _peopleProcessor.UpdatePerson(person);

            return result;
        }

        // PUT api/people/batch
        [HttpPut("batch")]
        public IActionResult BatchPut([FromBody]IEnumerable<People> people)
        {
            _peopleProcessor.BatchUpdatePeople(people);

            return Accepted();
        }

        // DELETE api/people/<peopleKey>
        [HttpDelete("{key}")]
        public People Delete(string peopleKey)
        {
            People person = _peopleProcessor.DeletePerson(Guid.Parse(peopleKey));

            return person;
        }

        // DELETE api/people/batch
        [HttpDelete("batch")]
        public IActionResult BatchDelete([FromBody]IEnumerable<string> peopleKeys)
        {
            _peopleProcessor.BatchDeletePeople(peopleKeys.Select(x => Guid.Parse(x)));

            return Accepted();
        }
    }
}
