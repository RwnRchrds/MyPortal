using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPortal.Database;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Caching;
using MyPortal.Logic.FileProviders;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Services;

namespace MyPortalWeb.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            DataConnectionFactory.ConnectionString = config.GetConnectionString("MyPortal");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("MyPortal")));

            // MyPortal Business Services
            services.AddBusinessServices();

            // MyPortal File Provider
            services.AddFileProvider(config);

            // MyPortal Cache
            services.AddScoped<IRolePermissionsCache, RolePermissionsCache>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAcademicYearService, AcademicYearService>();
            services.AddScoped<IAchievementService, AchievementService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IAttendanceMarkService, AttendanceMarkService>();
            services.AddScoped<IAttendanceWeekService, AttendanceWeekService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IDirectoryService, DirectoryService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<IIncidentService, IncidentService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILogNoteService, LogNoteService>();
            services.AddScoped<IPeriodService, AttendancePeriodService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IRegGroupService, RegGroupService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IStaffMemberService, StaffMemberService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISystemSettingService, SystemSettingService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IYearGroupService, YearGroupService>();
        }

        private static void AddFileProvider(this IServiceCollection services, IConfiguration config)
        {
            string provider = config["FileProvider:ProviderName"];

            switch (provider)
            {
                case "Google":
                    services.AddScoped<IHostedFileProvider, GoogleFileProvider>();
                    services.AddScoped<IFileService, HostedFileService>();
                    break;
                default:
                    services.AddScoped<ILocalFileProvider, LocalFileProvider>();
                    services.AddScoped<IFileService, LocalFileService>();
                    break;
            }
        }
    }
}
