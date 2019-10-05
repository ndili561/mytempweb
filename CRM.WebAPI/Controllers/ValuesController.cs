using System.Collections.Generic;
using System.Linq;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers
{
    //TODO Test controller - to be deleted
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // do not use repository directly in controller - test purpose
        private readonly IRepository _repository;

        public ValuesController(IRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _repository.Get<Person>().Select(p => p.Forename).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
