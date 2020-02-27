using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Authorisation;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Services;

namespace MyPortalCore
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("LiveConnection")));


            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                    {
                        options.SignIn.RequireConfirmedAccount = false;
                    })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyDictionary.UserType.Student, p => p.RequireClaim(ClaimTypeDictionary.UserType, UserTypeDictionary.Student));
                options.AddPolicy(PolicyDictionary.UserType.Staff, p => p.RequireClaim(ClaimTypeDictionary.UserType, UserTypeDictionary.Staff));
                options.AddPolicy(PolicyDictionary.UserType.Parent, p => p.RequireClaim(ClaimTypeDictionary.UserType, UserTypeDictionary.Parent));
            });

            // MyPortal Database Connection
            services.AddTransient<IDbConnection>(connection =>
                new SqlConnection(Configuration.GetConnectionString("LiveConnection")));

            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<ISchoolRepository, SchoolRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapAreaControllerRoute(
                    "Staff",
                    "Staff",
                    "Staff/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
