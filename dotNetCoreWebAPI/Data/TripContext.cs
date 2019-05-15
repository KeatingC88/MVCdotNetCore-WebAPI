using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;//
using dotNetCoreWebAPI.Models;//
using Microsoft.Extensions.DependencyInjection;

namespace dotNetCoreWebAPI.Data
{
    public class TripContext : DbContext
    {
        public TripContext(DbContextOptions<TripContext> options)
        : base(options) { }//WTF?

        public TripContext()
        {
            /*
             Used for when there is no reason to track the SHORT Context Queries that have no meaning to track.
             */
            //this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; 1)By Default: Do Not track the Queries
        }

        public DbSet<Trip> Trips { get; set; }

        //Seedings the Database?
        public static void SeedData(IServiceProvider serviceProvider)
        {
             using(var serviceScope = serviceProvider
            .GetRequiredService<IServiceScopeFactory>().CreateScope())
           {
                var context = serviceScope
                              .ServiceProvider.GetService<TripContext>();

                context.Database.EnsureCreated();

                if (context.Trips.Any()) return;//If there are Trips Do nothing

                context.Trips.AddRange(new Trip[]
                {
                new Trip{ Id = 0, Name = "Trip 1", StartDate = new DateTime(2018, 3, 5), EndDate = new DateTime(2018,3,8)},
                new Trip{ Id = 1, Name = "Trip 2", StartDate = new DateTime(2018, 3, 25), EndDate = new DateTime(2018, 3, 27)},
                new Trip{ Id = 2, Name = "Trip 3", StartDate = new DateTime(2018, 5, 7), EndDate = new DateTime(2018, 5, 9)}
                });

                context.SaveChanges();
            }
        }
        /*
            * *
            * For Avoiding Data Annotations Example mainly to avoid naming the Column Names in Controllers and having to change them if need be someday.
            * This is the solution to Overriding the ID UniqueKey Column to point to a different column AS the key.
            * *       
        */
        protected override void OnModelCreating(ModelBuilder mb)
        {
            //mb.Entity<Trip>().HasKey(t=>t.AlternativeID);
        }
    }//Class
}
