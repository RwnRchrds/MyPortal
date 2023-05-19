using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Database.Constants;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic;
using MyPortal.Logic.Configuration;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Identity;
using MyPortal.Logic.Interfaces;
using MyPortalWeb.Services;
using Task = System.Threading.Tasks.Task;

namespace MyPortalWeb.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<User, Role>(opt =>
                {
                    opt.Password = new PasswordOptions
                    {
                        RequireDigit = true,
                        RequireLowercase = true,
                        RequireNonAlphanumeric = true,
                        RequireUppercase = true,
                        RequiredLength = 6,
                        RequiredUniqueChars = 1
                    };
                })
                .AddSignInManager<ApplicationSignInManager>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = config["MyPortal:Url"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidTypes = new[] { "at+jwt" }
                    };
                });

            services.AddIdentityServer(options =>
                {
                    
                })
                .AddAspNetIdentity<User>()
                .AddProfileService<ProfileService>()
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext =
                        builder =>
                        {
                            switch (Configuration.Instance.DatabaseProvider)
                            {
                                case DatabaseProvider.MsSqlServer:
                                    builder.UseSqlServer(Configuration.Instance.ConnectionString);
                                    break;
                                default:
                                    throw new ConfigurationException("A database provider has not been set.");
                            }
                        };
                })
                .AddDeveloperSigningCredential();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.UserType.Staff,
                    policy => policy.RequireClaim(ApplicationClaimTypes.UserType, UserTypes.Staff.ToString()));
                options.AddPolicy(Policies.UserType.Student,
                    policy => policy.RequireClaim(ApplicationClaimTypes.UserType, UserTypes.Student.ToString()));
                options.AddPolicy(Policies.UserType.Parent,
                    policy => policy.RequireClaim(ApplicationClaimTypes.UserType, UserTypes.Parent.ToString()));
            });
            
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;    
                    return Task.CompletedTask;
                };
            });

            return services;
        }
    }
}
