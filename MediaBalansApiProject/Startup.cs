using MediaBalansApiProject.DAL;
using MediaBalansApiProject.Middlawares;
using MediaBalansApiProject.Models;
using MediaBalansApiProject.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace MediaBalansApiProject
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        MB_Context context;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(_configuration.GetSection("AzureAd"));

            context = services.BuildServiceProvider()
                      .GetService<MB_Context>();

            services.AddDbContext<MB_Context>(options => options.UseSqlServer(_configuration.GetConnectionString("connect")));

            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<MB_Context>()
                    .AddDefaultTokenProviders();

            services.AddScoped<UserService>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen();

            services.AddAuthentication();

            services.AddAuthorization();

            services.AddControllers();
        }


        // save db as admin 
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roleNames = { "Admin" };
            IdentityResult roleResult;
            try
            {
                foreach (var roleName in roleNames)
                {
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    // ensure that the role does not exist
                    if (!roleExist)
                    {
                        //create the roles and seed them to the database: 
                        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }



            // find the user with the admin email 
            var _user = await UserManager.FindByEmailAsync("admin@email.com");

            // check if the user exists
            if (_user == null)
            {
                //Here you could create the super admin who will maintain the web app
                var poweruser = new User
                {
                    UserName = "Admin",
                    Email = "admin@email.com",
                };
                string adminPassword = "123Kamran@";

                var createPowerUser = await UserManager.CreateAsync(poweruser, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }

            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ReguestResponceMiddleware>();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            CreateRoles(serviceProvider).GetAwaiter().GetResult();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");

            });
        }
    }
}
