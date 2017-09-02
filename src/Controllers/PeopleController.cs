using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcorecrud.DomainModel;
using dotnetcorecrud.Processors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace dotnetcorecrud.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly TimeSpan DefaultMemoryCacheExpiration = TimeSpan.FromSeconds(24 * 3600);

        private IMemoryCache _memoryCache;
        
        private readonly IPeopleProcessor _peopleProcessor;

        public PeopleController(IMemoryCache memoryCache, IPeopleProcessor peopleProcessor)
        {
            _memoryCache = memoryCache;
            _peopleProcessor = peopleProcessor;
        }

        // GET api/people/all
        [HttpGet("all")]
        public IEnumerable<People> GetAll()
        {
            IEnumerable<People> result = _peopleProcessor.GetAllPeople();
            
            return result;
        }

        // GET api/people/batch/<jobKey>
        [HttpGet("batch/{jobKey}")]
        public IActionResult GetBatchJobStatus(string jobKey)
        {
            JobResult jobResult;
            IActionResult actionResult = NoContent();

            bool isValid = _memoryCache.TryGetValue<JobResult>(Guid.Parse(jobKey), out jobResult);

            if (isValid)
            {
                actionResult = Ok(jobResult);
            }
            
            return actionResult;
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
            Guid jobKey = _peopleProcessor.BatchCreatePeople(peopleNames);

            IDictionary<string, string> responseDict = new Dictionary<string, string>();
            responseDict.Add("JobKey", jobKey.ToString());

            var cacheEntryOptions = 
                new MemoryCacheEntryOptions().SetSlidingExpiration(DefaultMemoryCacheExpiration); 

            _memoryCache.Set(jobKey, new JobResult("POST", false), cacheEntryOptions);

            return Accepted(responseDict);
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
            Guid jobKey = _peopleProcessor.BatchUpdatePeople(people);
            
            IDictionary<string, string> responseDict = new Dictionary<string, string>();
            responseDict.Add("JobKey", jobKey.ToString());

            var cacheEntryOptions = 
                new MemoryCacheEntryOptions().SetSlidingExpiration(DefaultMemoryCacheExpiration); 

            _memoryCache.Set(jobKey, new JobResult("PUT", false), cacheEntryOptions);

            return Accepted(responseDict);
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
            Guid jobKey = 
                _peopleProcessor.BatchDeletePeople(peopleKeys.Select(x => Guid.Parse(x)));

            IDictionary<string, string> responseDict = new Dictionary<string, string>();
            responseDict.Add("JobKey", jobKey.ToString());

            var cacheEntryOptions = 
                new MemoryCacheEntryOptions().SetSlidingExpiration(DefaultMemoryCacheExpiration); 

            _memoryCache.Set(jobKey, new JobResult("DELETE", false), cacheEntryOptions);

            return Accepted(responseDict);
        }
    }
}
