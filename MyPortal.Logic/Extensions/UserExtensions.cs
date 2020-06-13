using System;
using System.Linq;
using System.Security.Claims;
using Google.Apis.Drive.v3.Data;
using MyPortal.Database.Constants;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Helpers;
using ClaimTypes = MyPortal.Database.Constants.ClaimTypes;

namespace MyPortal.Logic.Extensions
{
    public static class UserExtensions
    {
        public static string GetDisplayName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.DisplayName)?.Value ?? user.Identity.Name;
        }

        public static bool HasPermission(this ClaimsPrincipal principal, params Guid[] permissions)
        {
            if (permissions.Length == 0)
            {
                return true;
            }

            foreach (var permission in permissions)
            {
                if (Permissions.ClaimValues.TryGetValue(permission, out var claimValue) &&
                    principal.HasClaim(ClaimTypes.Permission, claimValue))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsType(this ClaimsPrincipal principal, string userType)
        {
            return principal.HasClaim(ClaimTypes.UserType, userType);
        }
    }
}