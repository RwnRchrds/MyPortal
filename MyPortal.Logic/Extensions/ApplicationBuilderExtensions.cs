using Microsoft.AspNetCore.Builder;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Middleware;

namespace MyPortal.Logic.Extensions;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds the PermissionMiddleware to allow restricting API endpoints to users with certain permissions,
    /// and the FileProviderMiddleware to allow restricting API endpoints to a certain file provider configuration.
    /// </summary>
    /// <param name="app">
    /// The IApplicationBuilder to add the middleware to.
    /// </param>
    /// <returns></returns>
    public static IApplicationBuilder UseMyPortal(this IApplicationBuilder app)
    {
        app.UseMiddleware<PermissionMiddleware>();
        app.UseMiddleware<FileProviderMiddleware>();

        return app;
    }
}