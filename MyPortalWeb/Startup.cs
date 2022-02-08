using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyPortalWeb.Extensions;
using MyPortalWeb.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;

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
            services.AddApplicationServices(Configuration);

            services.AddControllers();
            services.AddCors();

            services.AddIdentityServices(Configuration);

            services.AddBusinessServices();

#if DEBUG
            services.AddSwaggerGen(c =>
            {
                c.CustomOperationIds(e => e.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "3.1.0",
                    Title = "MyPortal",
                    Description = "MyPortal API"
                });

                //var filePath = Path.Combine("MyPortalWeb.xml");
                //c.IncludeXmlComments(filePath);
            });
#endif
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
#if DEBUG
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyPortal API v1");
                c.RoutePrefix = "api";
            });
#endif
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseMiddleware<PermissionMiddleware>();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
