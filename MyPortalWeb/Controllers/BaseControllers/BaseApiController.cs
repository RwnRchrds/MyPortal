using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;

namespace MyPortalWeb.Controllers.BaseControllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase, IDisposable
    {
        protected readonly IAppServiceCollection Services;
        private readonly IRolePermissionsCache _rolePermissionsCache;

        public BaseApiController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache)
        {
            Services = services;
            _rolePermissionsCache = rolePermissionsCache;
        }

        public virtual void Dispose()
        {
            Services.Dispose();
        }

        protected async Task<IActionResult> ProcessAsync(Func<Task<IActionResult>> method, params Guid[] permissionsRequired)
        {
            if (await UserHasPermission(permissionsRequired))
            {
                try
                {
                    return await method.Invoke();
                }
                catch (Exception e)
                {
                    return HandleException(e);
                }
            }

            return Forbid();
        }

        protected async Task<UserModel> GetLoggedInUser()
        {
            return await Services.Users.GetUserByPrincipal(User);
        }

        protected async Task<bool> UserHasPermission(params Guid[] permissionIds)
        {
            if (!permissionIds.Any())
            {
                return true;
            }

            var roleClaims = User.FindAll(c => c.Type == ClaimTypes.Role);

            foreach (var roleClaim in roleClaims)
            {
                if (Guid.TryParse(roleClaim.Value, out Guid roleId))
                {
                    var rolePermissions = await _rolePermissionsCache.GetPermissions(roleId);

                    if (permissionIds.Any(rolePermissions.Contains))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private IActionResult HandleException(Exception ex)
        {
            HttpStatusCode statusCode;

            var message = ex.GetBaseException().Message;

            switch (ex)
            {
                case NotFoundException n:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case SecurityTokenException s:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case UnauthorisedException u:
                    statusCode = HttpStatusCode.Forbidden;
                    break;
                default:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
            }

            switch (statusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound(message);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(message);
                case HttpStatusCode.Forbidden:
                    return Forbid(message);
                default:
                    return BadRequest(message);
            }
        }
    }
}