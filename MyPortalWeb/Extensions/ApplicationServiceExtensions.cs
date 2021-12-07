using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPortal.Database.Models;
using MyPortal.Logic;
using MyPortal.Logic.Enums;
using MyPortal.Logic.FileProviders;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Configuration;
using MyPortal.Logic.Services;

namespace MyPortalWeb.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("MyPortal")));

            // MyPortal Configuration Settings
            SetConfiguration(config);

            return services;
        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAcademicYearService, AcademicYearService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IBehaviourService, BehaviourService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ICurriculumService, CurriculumService>();
            services.AddScoped<IDirectoryService, DirectoryService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILogNoteService, LogNoteService>();
            services.AddScoped<IParentEveningService, ParentEveningService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IRegGroupService, RegGroupService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<ISenService, SenService>();
            services.AddScoped<IStaffMemberService, StaffMemberService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISystemSettingService, SystemSettingService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IYearGroupService, YearGroupService>();

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
        }

        private static void SetConfiguration(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("MyPortal");

            Configuration.Instance.ConnectionString = connectionString;

            Configuration.Instance.InstallLocation = Environment.CurrentDirectory;

            switch (config["FileProvider:ProviderName"])
            {
                case "Google":
                    Configuration.Instance.FileProvider = FileProvider.GoogleDrive;
                    break;
                default:
                    Configuration.Instance.FileProvider = FileProvider.Local;
                    break;
            }

            var googleCredPath = config["Google:CredentialPath"];

            if (!string.IsNullOrWhiteSpace(googleCredPath))
            {
                Configuration.Instance.GoogleConfig = new GoogleConfig
                {
                    CredentiaPath = googleCredPath,
                    DefaultAccountName = config["Google:DefaultAccountName"]
                };
            }
        }
    }
}
