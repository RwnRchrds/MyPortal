using System;
using System.Collections;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Extensions
{
    public static class UserExtensions
    {
        public static bool IsType(this ClaimsPrincipal principal, int userType)
        {
            var hasType = int.TryParse(principal.FindFirst(ApplicationClaimTypes.UserType).Value, out var claimValue);

            return hasType && claimValue == userType;
        }

        public static async Task<bool> HasPermission(this ClaimsPrincipal principal, IRoleService roleService,
            PermissionRequirement requirement, params PermissionValue[] permissionValues)
        {
            if (!permissionValues.Any())
            {
                return true;
            }

            var roleClaims = principal.FindAll(c => c.Type == ClaimTypes.Role);

            foreach (var roleClaim in roleClaims)
            {
                if (Guid.TryParse(roleClaim.Value, out Guid roleId))
                {
                    var role = await roleService.GetRoleById(roleId);

                    var rolePermissions = new BitArray(role.Permissions);

                    foreach (var permissionValue in permissionValues)
                    {
                        if (rolePermissions[(int)permissionValue])
                        {
                            if (requirement == PermissionRequirement.RequireAny)
                            {
                                return true;   
                            }
                        }
                        else if (requirement == PermissionRequirement.RequireAll)
                        {
                            return false;
                        }
                    }
                }
            }

            return requirement == PermissionRequirement.RequireAll;
        }
    }
}