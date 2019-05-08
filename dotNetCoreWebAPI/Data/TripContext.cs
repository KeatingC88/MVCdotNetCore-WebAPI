using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;//
using dotNetCoreWebAPI.Models;//

namespace dotNetCoreWebAPI.Data
{
    public class TripContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        /*
            * *
            * For Avoiding Data Annotations Example mainly to avoid naming the Column Names in Controllers and having to change them if need be someday.
            * This is the solution to Overriding the ID UniqueKey Column to point to a different column AS the key.
            * *       
        protected override void OnModelCreating(ModelBuilder mb)
        {            
            //mb.Entity<Trip>().HasKey(t=>t.AlternativeID);
        }
        */
    }
}
