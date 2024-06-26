using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MyPortalWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build();

                    var portNumber = config.GetValue<int>("WebHost:Port");
                    var http = config.GetValue<bool>("WebHost:UseHttps") ? "https" : "http";

                    webBuilder.UseUrls($"{http}://*:{portNumber}");
                });
    }
}