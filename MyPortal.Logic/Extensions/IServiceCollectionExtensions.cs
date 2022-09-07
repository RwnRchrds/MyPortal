using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPortal.Database.Models;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.FileProviders;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Configuration;
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
                    case DatabaseProvider.MySql:
                        options.UseMySQL(Configuration.Instance.ConnectionString);
                        break;
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

            if (Configuration.Instance.FileProvider == FileProvider.Local)
            {
                services.AddScoped<ILocalFileProvider, LocalFileProvider>();
                services.AddScoped<IFileService, LocalFileService>();
            }
            else
            {
                if (Configuration.Instance.FileProvider == FileProvider.GoogleDrive)
                {
                    services.AddScoped<IHostedFileProvider, GoogleFileProvider>();
                }

                services.AddScoped<IFileService, HostedFileService>();
            }

            return services;
        }

        private static void SetFileProvider(string fileProvider)
        {
            switch (fileProvider.ToLower())
            {
                case "google":
                    Configuration.Instance.FileProvider = FileProvider.GoogleDrive;
                    break;
                case "local":
                    Configuration.Instance.FileProvider = FileProvider.Local;
                    break;
                default:
                    throw new ArgumentException($"The file storage provider {fileProvider} is invalid.");
            }
        }

        private static void SetConfiguration(IConfiguration config)
        {
            var databaseProvider = config["Database:Provider"];
            var connectionString = config["Database:ConnectionString"];
            var fileEncryptionKey = config["FileStorage:EncryptionKey"];
            var fileProvider = config["FileStorage:Provider"];

            Configuration.CreateInstance(databaseProvider, connectionString);

            Configuration.Instance.InstallLocation = Environment.CurrentDirectory;
            
            SetFileProvider(fileProvider);

            Configuration.Instance.FileEncryptionKey = fileEncryptionKey;

            var googleCredPath = config["Google:CredentialPath"];

            if (!string.IsNullOrWhiteSpace(googleCredPath))
            {
                Configuration.Instance.GoogleConfig = new GoogleConfig
                {
                    CredentialPath = googleCredPath,
                    DefaultAccountName = config["Google:DefaultAccountName"]
                };
            }
        }
    }
}
