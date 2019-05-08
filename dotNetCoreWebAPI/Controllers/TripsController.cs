using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetCoreWebAPI.Models;//Include Models Folder
using dotNetCoreWebAPI.Data;//Include Data Folder
using Microsoft.EntityFrameworkCore;



namespace dotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
               TripContext _dbContext;//channel to the database
        public TripsController(TripContext context)
        {
            _dbContext = context;//channel to the database
        }
        //private Repository _repository;//(An Old Method to the Repo that used to be injected in the Startup.cs Configure_Services method.)
        // GET api/Trips
        [HttpGet]
        public IEnumerable<Trip>Get()
        {
            return _dbContext.Trips.ToList();//Force Execution and closes connection, only return Results View from the Database. The Browser doesn't have entity framework...
        }

        // GET api/Trips/5
        [HttpGet("{id}")]
        public Trip Get(int id)
        {
            //return _repository.GetById(id); Old method
            return _dbContext.Trips.Find(id);
        }

        // POST api/Trips
        [HttpPost]
        public void Post([FromBody]Trip value)
        {
            _dbContext.Trips.Add(value);
        }

        // PUT api/Trips/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Trip value)
        {
            _dbContext.Trips.Update(value);
        }

        // DELETE api/Trips/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //_dbContext.Trips.Remove(id);
        }
    }
}
