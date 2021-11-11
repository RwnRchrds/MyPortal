using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Database.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortalWeb.Controllers.BaseControllers
{
    
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IUserService UserService;
        protected IRoleService RoleService;

        public BaseApiController(IUserService userService, IRoleService roleService)
        {
            UserService = userService;
            RoleService = roleService;
        }

        protected async Task<UserModel> GetLoggedInUser()
        {
            return await UserService.GetUserByPrincipal(User);
        }

        protected async Task<bool> UserHasPermission(params PermissionValue[] permissionValues)
        {
            if (!permissionValues.Any())
            {
                return true;
            }

            var roleClaims = User.FindAll(c => c.Type == ClaimTypes.Role);

            foreach (var roleClaim in roleClaims)
            {
                if (Guid.TryParse(roleClaim.Value, out Guid roleId))
                {
                    var role = await RoleService.GetRoleById(roleId);

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

        protected IActionResult HandleException(Exception ex)
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

            return StatusCode((int) statusCode, message);
        }

        protected IActionResult Error(int statusCode, object returnObject)
        {
            return StatusCode(statusCode, returnObject);
        }
    }
}