using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using dotNetCoreWebAPI.Data;
using dotNetCoreWebAPI.Controllers;


namespace dotNetCoreWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<Models.Repository>();//Add a Class by Startup Injection
            services.AddDbContext<TripContext>(options => options.UseSqlite("Data Source = listOfTrips.db"));//Use SQlite Database
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Swagger SwashBuckle
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Info { Title = "Trip Tracker", Version = "v1" })
            );
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {            
            app.UseSwagger();//Must be above the app.Mvc() method.
            
            if (env.IsDevelopment() || env.IsStaging())
            { //When Server is Hosting
                app.UseSwaggerUI(options =>
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Trip Tracker v1")
                );
            }
            
            if (env.IsDevelopment())
            {//App default condition
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //App defaults
            app.UseHttpsRedirection();
            app.UseMvc();
            //Manual Injection for Seeding Data if the Database is Empty
            TripContext.SeedData(app.ApplicationServices);
        }
    }
}
