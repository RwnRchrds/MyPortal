using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Response;

namespace MyPortal.Logic.Middleware
{
    public class PermissionMiddleware
    {
        private RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            Endpoint endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            PermissionAttribute attribute = endpoint?.Metadata.GetMetadata<PermissionAttribute>();

            if (attribute != null)
            {
                if (context.User.IsAuthenticated() &&
                    await context.User.HasPermission(userService, attribute.Requirement, attribute.Permissions))
                {
                    await _next(context);
                    return;
                }

                context.Response.StatusCode = 403;
                var error = new ErrorResponseModel("You do not have permission to access this resource.");
                await context.Response.WriteJsonAsync(error);
                context.Response.ContentLength = context.Response.Body.Length;
                await context.Response.CompleteAsync();
                return;
            }

            if (!context.Response.HasStarted)
            {
                await _next(context);
            }
        }
    }
}