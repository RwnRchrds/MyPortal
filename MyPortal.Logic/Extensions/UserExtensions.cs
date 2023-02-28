using System;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Extensions
{
    public static class UserExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var nameId = principal.Claims.FirstOrDefault(c => c.Type.Contains(JwtRegisteredClaimNames.NameId));

            if (nameId == null)
            {
                throw new SecurityTokenException("User ID could not be retrieved from token.");
            }

            var tokenValid = Guid.TryParse(nameId.Value, out var userId);

            if (!tokenValid)
            {
                throw new SecurityTokenException("User ID could not be retrieved from token.");
            }

            return userId;
        }
        
        public static bool IsType(this ClaimsPrincipal principal, int userType)
        {
            var hasType = int.TryParse(principal.FindFirst(ApplicationClaimTypes.UserType).Value, out var claimValue);

            return hasType && claimValue == userType;
        }

        public static async Task<bool> HasPermission(this ClaimsPrincipal principal, IUserService userService, 
            PermissionRequirement requirement, params PermissionValue[] permissionValues)
        {
            return await PermissionHelper.UserHasPermission(principal.GetUserId(), userService, requirement,
                permissionValues);
        }
    }
}