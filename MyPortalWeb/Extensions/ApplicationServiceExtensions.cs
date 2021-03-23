using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPortal.Database.Models;
using MyPortal.Logic;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Configuration;

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
            services.AddScoped<IAppServiceCollection, AppServiceCollection>();
        }

        private static void SetConfiguration(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("MyPortal");

            Configuration.Instance.ConnectionString = connectionString;

            Configuration.Instance.InstallLocation = config["MyPortal:InstallLocation"];

            Configuration.Instance.TokenKey = config["MyPortal:TokenKey"];

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
