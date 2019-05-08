using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetCoreWebAPI.Models;
using dotNetCoreWebAPI.Data;
using Microsoft.EntityFrameworkCore;



namespace dotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
               TripContext _dbContext;
        public TripsController(TripContext context)
        {
            _dbContext = context;
        }
        // GET api/Trips
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var trips = await _dbContext.Trips
                .AsNoTracking()
                .ToListAsync();
            return Ok(trips);
        }

        // GET api/Trips/5
        [HttpGet("{id}")]
        public Trip Get(int id)
        {
            return _dbContext.Trips.Find(id);//Entity's way of GetByID
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
