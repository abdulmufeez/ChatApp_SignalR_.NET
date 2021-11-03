using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp
{
    public class Startup
    {
        public IConfiguration _config { get; set; }
        public Startup(IConfiguration config) => _config = config;            //this configuartions is created specially for npgsql
                
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);              //for Model-view-controller
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(_config.GetConnectionString("DefaultConnection")));   //telling database to configure database using AppDbContext
            services.AddIdentity<User, IdentityRole>( options => {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })                                      //Adding users as user role using IdentityRole
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();                   //for using CSS files 
            app.UseAuthentication();                //for authentication like signin and login user roles

            app.UseMvcWithDefaultRoute();           //for default routing
            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapGet("/", async context =>
            //     {
            //         await context.Response.WriteAsync("Hello World!");
            //     });
            // });
        }
    }
}
