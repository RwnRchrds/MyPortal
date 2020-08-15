using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyPortal.Database.Constants;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Authorisation;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
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
            DataConnectionFactory.ConnectionString = Configuration.GetConnectionString("MyPortal");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MyPortal")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders();

            services.Configure<CookieAuthenticationOptions>(options =>
            {
                options.AccessDeniedPath = "/AccessDenied";
            });

            services.AddControllersWithViews();
            services.AddMvc();
            services.AddRazorPages();

            services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.UserType.Student,
                    p => p.RequireClaim(ClaimTypes.UserType, UserTypes.Student));
                options.AddPolicy(Policies.UserType.Staff, p => p.RequireClaim(ClaimTypes.UserType, UserTypes.Staff));
                options.AddPolicy(Policies.UserType.Parent, p => p.RequireClaim(ClaimTypes.UserType, UserTypes.Parent));

                // Get claim values for permission-based authorisation
                using (var context = DataConnectionFactory.CreateContext())
                {
                    var permissionsInDb = context.ApplicationPermissions.ToList();

                    Permissions.PopulateClaimValues();

                    foreach (var perm in permissionsInDb)
                    {
                        Permissions.ClaimValues[perm.Id] = perm.ClaimValue;
                    }

                    if (Permissions.ClaimValues.Any(x => x.Value == string.Empty))
                    {
                        throw new Exception(
                            "Some permissions have not been mapped correctly. Please contact your MyPortal support team.");
                    }
                }
            });

            // MyPortal database connection
            services.AddScoped<IDbConnection>(connection =>
                new SqlConnection(Configuration.GetConnectionString("MyPortal")));

            // MyPortal business services
            services.AddScoped<IAcademicYearService, AcademicYearService>();
            services.AddScoped<IAchievementService, AchievementService>();
            services.AddScoped<IApplicationRolePermissionService, ApplicationPermissionService>();
            services.AddScoped<IApplicationRoleService, ApplicationRoleService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IAttendanceMarkService, AttendanceMarkService>();
            services.AddScoped<IAttendanceWeekService, AttendanceWeekService>();
            services.AddScoped<IDirectoryService, DirectoryService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IIncidentService, IncidentService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILogNoteService, LogNoteService>();
            services.AddScoped<IPeriodService, AttendancePeriodService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IStaffMemberService, StaffMemberService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISystemSettingService, SystemSettingService>();
            services.AddScoped<ITaskService, TaskService>();
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
                endpoints.MapAreaControllerRoute(
                    "Students",
                    "Students",
                    "Students/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                    "Parents",
                    "Parents",
                    "Parents/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
