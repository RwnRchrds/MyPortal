using System;
using System.Collections;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using MyPortal.Database.Enums;
using MyPortal.Logic.Interfaces.Services;
using MyPortalWeb.Attributes;

namespace MyPortalWeb.Middleware
{
    public class PermissionMiddleware
    {
        private RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IRoleService roleService)
        {
            Endpoint endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            PermissionAttribute attribute = endpoint?.Metadata.GetMetadata<PermissionAttribute>();

            if (attribute != null)
            {
                if (context.User.IsAuthenticated() && await UserHasPermission(roleService, context.User, attribute.Permissions))
                {
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("You do not have permission to access this resource.");
                }
            }

            await _next(context);
        }
        
        protected async Task<bool> UserHasPermission(IRoleService roleService, ClaimsPrincipal user, PermissionValue[] permissionValues)
        {
            if (!permissionValues.Any())
            {
                return true;
            }

            var roleClaims = user.FindAll(c => c.Type == ClaimTypes.Role);

            foreach (var roleClaim in roleClaims)
            {
                if (Guid.TryParse(roleClaim.Value, out Guid roleId))
                {
                    var role = await roleService.GetRoleById(roleId);

                    var rolePermissions = new BitArray(role.Permissions);

                    foreach (var permissionValue in permissionValues)
                    {
                        if (rolePermissions[(int) permissionValue])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}