using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPortal.Database.Models;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Services;

namespace MyPortal.Logic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyPortal(this IServiceCollection services, IConfiguration config)
        {
            SetConfiguration(config);

            services.AddApplicationDbContext();
            services.AddBusinessServices();

            return services;
        }

        private static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
        {
            Configuration.CheckConfiguration();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                switch (Configuration.Instance.DatabaseProvider)
                {
                    case DatabaseProvider.MsSqlServer:
                        options.UseSqlServer(Configuration.Instance.ConnectionString);
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

            return services;
        }

        private static string GetSecret(IConfiguration config, string configKey, string secretName)
        {
            var secretSource = config[configKey];

            if (secretSource.ToLower() == "azure")
            {
                var keyVaultName = Environment.GetEnvironmentVariable("MYPORTAL_KEYVAULT");
                var keyVaultSecret = Environment.GetEnvironmentVariable($"MYPORTAL_{secretName.ToUpper()}");

                var secret = AzureKeyVaultHelper.GetSecret(keyVaultName, keyVaultSecret);

                return secret;
            }   
            
            if (secretSource.ToLower() == "environment")
            {
                var secret = Environment.GetEnvironmentVariable($"MYPORTAL_{secretName.ToUpper()}");
                
                return secret;
            }
            
            return secretSource;
        }

        private static string GetConnectionString(IConfiguration config)
        {
            return GetSecret(config, "DataSource:ConnectionString", "cs");
        }

        private static string GetFileEncryptionKey(IConfiguration config)
        {
            return GetSecret(config, "DataSource:FileEncryptionKey", "fek");
        }

        private static void SetConfiguration(IConfiguration config)
        {
            var connectionString = GetConnectionString(config);
            var fileEncryptionKey = GetFileEncryptionKey(config);
            
            var databaseProvider = config["DataSource:DbProvider"];
            var fileProvider = config["DataSource:FileProvider"];

            Configuration.CreateInstance(databaseProvider, fileProvider, connectionString);
            
            Configuration.Instance.InstallLocation = Environment.CurrentDirectory;
            Configuration.Instance.FileEncryptionKey = fileEncryptionKey;
        }
    }
}
