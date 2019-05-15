using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using dotNetCoreWebAPI.Models;
using dotNetCoreWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace dotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        private readonly TripContext _dbContext;
        public TripsController(TripContext tripContext)
        {
            _dbContext = tripContext;
            //_dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; 2) Can be used here too instead of in TripContext.cs
        }
        // GET api/Trips
        [HttpGet]
        /*
        1) ToList Async LINQ
        public async Task<IEnumerable<Models.Trip>> GetAsync()
        {
            return await _dbContext.Trips.ToListAsync();
        }
        */
        /*2) ToList Async Collection IEnumerable<T>
        public async Task<IEnumerable<Models.Trip>> GetAsync()
        {
            return await _dbContext.Trips
                .AsNoTracking() //Entity Executes queries and is tracking them with ChangeTracker Class by "Eject the Trip Context"
                .ToListAsync();
        }
        3) ToList Async to IActionResult to return Status Codes using OK() Helper (Do not need to create an HTTP Result)            
           When you Execute this Query, Don't bother building the ChangeTracker Objects (Saves Resources)
        */
        public async Task<IActionResult> GetAsync()
        {
            var trips = await _dbContext.Trips//Var used at Design Time Compiler will resolve to the appropriate data type
                .AsNoTracking()
                .ToListAsync();
            return Ok(trips);
        }
        // GET api/Trips/5
        [HttpGet("{id}")]
        public Trip Get(int id)
        {
            return _dbContext.Trips.Find(id);//Find(id) is not on a DBSET!!! and does not flag an error!
        }

        // POST api/Trips
        [HttpPost]
        public IActionResult Post([FromBody]Trip value)
        {
            //_dbContext.Add(value);Did not specify or use Trips Property containing the DbSet<t> in TripContext.cs and does not flag and error
            if (!ModelState.IsValid)//This Validates the Class [Required] Attributes in Trips.CS
                return BadRequest(ModelState);
            _dbContext.Trips.Add(value);//Saves Redundent Code and Great for writing Generic Code. Enforces objects to use the Trip DbSet in TripContext.cs
            _dbContext.SaveChanges();//Save All Context Data
            return Ok();
        }

        // PUT api/Trips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Trip value)
        {
            //_dbContext.Trips.Attach(value); 1) Older
            //_dbContext.Entry(value).State = EntityState.Modified; 2) Oldest
            /*
                Writing it this way will avoid ChangeTracker by defining them directly in this void but if the Tracker is off... then why?
                var tripFromDb = _dbContext.Trips.Find(id);
                //LeftRight Comparison
                tripFromDb.Name = value.Name;
                tripFromDb.StartDate = value.StartDate;
                _dbContext.SaveChanges();
             */
            //_dbContext.Trips.Update(value);Direct and UnClean to do. Use someting like that returns from _repo.Update(value);
            //EFCore1 Modern Way for a simple null and validation using the Required Class Attributes check
            //if (_dbContext.Trips.Find(id) == null)//Check for a Simple Null
            if (!_dbContext.Trips.Any(t => t.Id == id))//This blocks a null w/ a bool and what if they fire 2 queries instead of 1...?
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Save Async
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/Trips/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var myTrip = _dbContext.Trips.Find(id);

            if (myTrip == null)
                return NotFound();

            // Delete From Trips Where id = ?
            _dbContext.Trips.Remove(myTrip);//EFcore needs the Object in order for it to Track the object and Mark it as Deleted in ChangeTracker Class
            _dbContext.SaveChanges();//Pass the Delete Statement back to the Database. 
            //Automatic Seedings is coming in 2.1 as it's not reliable today. So it's written manually in the TripContext.cs to seed the Database.
            return NoContent();
        }
    }
}
