using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace dotNetCoreWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        /*
            "Swagger"(SwashBuckle) https://github.com/domaindrivendev/Swashbuckle.AspNetCore
        */
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Creating a Global "Model.Repository" object for the application reference the Repository.
            services.AddTransient<Models.Repository/*Generic Type*/>();//Injection Service Model
            //App default
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Swagger Generator and Document Object 
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Info { Title = "Trip Tracker", Version = "v1" })
            );//(Only use Test Data when using Swagger between devleopers as it will update your database if connected to the application)

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /*
             *  Must be above the app.Mvc() method or it will overwrite your current routes. This is testing in Swagger UI.
            */
            app.UseSwagger();
            if (env.IsDevelopment() || env.IsStaging()) { //Control when it's exposed?
                app.UseSwaggerUI(options =>
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Trip Tracker v1")
                );
            }
            
            //App default condition
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //App default methods
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
