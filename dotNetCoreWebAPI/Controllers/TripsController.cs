using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetCoreWebAPI.Models;// Import/Include Models Folder

namespace dotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        
        public TripsController(Repository repository)//Use the injection service
        {
            _repository = repository;//Set Repo to Private Property
        }

        private Repository _repository;//Declare
        // GET api/Trips
        [HttpGet]
        public IEnumerable<Trip>Get()
        {
            return _repository.Get();
        }

        // GET api/Trips/5
        [HttpGet("{id}")]
        public Trip Get(int id)
        {
            return _repository.GetById(id);
        }

        // POST api/Trips
        [HttpPost]
        public void Post([FromBody]Trip value)
        {
            _repository.Add(value);
        }

        // PUT api/Trips/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Trip value)
        {
            _repository.Update(value);
        }

        // DELETE api/Trips/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Remove(id);
        }
    }
}
