using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Database.Models;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Services;
using Task = System.Threading.Tasks.Task;

namespace MyPortalWeb
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

            services.AddControllers();
            services.AddCors();

            // MyPortal Database Connection
            services.AddScoped<IDbConnection>(connection =>
                new SqlConnection(Configuration.GetConnectionString("MyPortal")));

            // MyPortal Business Services
            services.AddScoped<IAcademicYearService, AcademicYearService>();
            services.AddScoped<IAchievementService, AchievementService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
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
            services.AddScoped<IUserService, UserService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(Configuration.GetSection("MyPortal:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = c =>
                        {
                            if (c.Exception is SecurityTokenExpiredException)
                            {
                                c.Response.Headers.Add("Token-Expired", "true");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
