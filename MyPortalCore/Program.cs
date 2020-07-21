using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyPortalCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args);

            if (Debugger.IsAttached || args.Contains("--debug"))
            {
                var host = builder.Build();

                host.Run();
            }
            else
            {
                RunAsService(builder);
            }
        }

        public static void RunAsService(IHostBuilder builder)
        {
            var host = builder.UseWindowsService().Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build();

                    var portNumber = config.GetValue(typeof(int), "WebHost:Port");

                    var http = config.GetValue<bool>("WebHost:UseHttps") ? "https" : "http";

                    webBuilder.UseUrls($"{http}://*:{portNumber}");
                });
    }
}
