using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPortal.Database.Models;
using MyPortal.Logic.Configuration;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.FileProviders;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Identity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Services;

namespace MyPortal.Logic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyPortal(this IServiceCollection services, string connectionString)
        {
            var builder = new ConfigBuilder(connectionString);
            
            builder.Build();

            services.AddApplicationDbContext();
            services.AddBusinessServices();

            return services;
        }
        
        public static IServiceCollection AddMyPortal(this IServiceCollection services, Action<ConfigBuilder> configBuilder)
        {
            var builder = new ConfigBuilder("");

            configBuilder(builder);
            
            builder.Build();

            services.AddApplicationDbContext();
            services.AddBusinessServices();

            return services;
        }

        private static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
        {
            Configuration.Configuration.CheckConfiguration();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                switch (Configuration.Configuration.Instance.DatabaseProvider)
                {
                    case DatabaseProvider.MsSqlServer:
                        options.UseSqlServer(Configuration.Configuration.Instance.ConnectionString);
                        break;
                    /*case DatabaseProvider.MySql:
                        options.UseMySQL(Configuration.Instance.ConnectionString);
                        break;*/
                    default:
                        throw new ConfigurationException("A database provider has not been set.");
                }
            });

            return services;
        }

        private static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddTransient(s => s.GetService<HttpContext>()?.User);
            services.AddHttpContextAccessor();
            services.AddScoped<ISessionUser, HttpSessionUser>();
            services.AddScoped<IAcademicYearService, AcademicYearService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IBehaviourService, BehaviourService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ICurriculumService, CurriculumService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILogNoteService, LogNoteService>();
            services.AddScoped<IParentEveningService, ParentEveningService>();
            services.AddScoped<IPastoralService, PastoralService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<ISenService, SenService>();
            services.AddScoped<IStaffMemberService, StaffMemberService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISystemSettingService, SystemSettingService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IUserService, UserService>();

            if (Configuration.Configuration.Instance.FileProvider == FileProvider.GoogleDrive)
            {
                services.AddScoped<IFileService, HostedFileService>();
                services.AddScoped<IHostedFileProviderFactory, HttpHostedFileProviderFactory>();
            }
            else
            {
                services.AddScoped<IFileService, LocalFileService>();
                services.AddScoped<ILocalFileProvider, LocalFileProvider>();
            }

            return services;
        }
    }
}
