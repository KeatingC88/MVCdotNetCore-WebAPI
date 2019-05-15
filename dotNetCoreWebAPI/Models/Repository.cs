using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreWebAPI.Models
{
    public class Repository
    {
        //Test Data Sample
        private List<Trip> listOfTrips = new List<Trip>
        {
            new Trip{ Id = 0, Name = "Trip 1", StartDate = new DateTime(2018, 3, 5), EndDate = new DateTime(2018,3,8)},
            new Trip{ Id = 1, Name = "Trip 2", StartDate = new DateTime(2018, 3, 25), EndDate = new DateTime(2018, 3, 27)},
            new Trip{ Id = 2, Name = "Trip 3", StartDate = new DateTime(2018, 5, 7), EndDate = new DateTime(2018, 5, 9)}
        };
        //Controler for Data
        public List<Trip> Get()
        {
            return listOfTrips.ToList();
        }
        
        public Trip GetById(int id)
        {
            return listOfTrips.First(t => t.Id == id);
        }

        public void Add(Trip newTrip)
        {
            listOfTrips.Add(newTrip);
        }

        public void Update(Trip tripToUpdate)
        {
            //This way for "Put" is For Test Data Only!
            listOfTrips.Remove(listOfTrips.First(t => t.Id == tripToUpdate.Id));
            Add(tripToUpdate);
        }

        public void Remove(int id)
        {
            listOfTrips.Remove(listOfTrips.First(t => t.Id == id));
        }
    }
}
