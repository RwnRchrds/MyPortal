using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Models;

namespace MyPortal.Services
{
    public static class ValidationService
    {
        public static List<string> ErrorMessages = new List<string>();

        public static bool HasPermission(this IPrincipal principal, string permission)
        {
            var identity = new IdentityContext();
            var roleStore = new RoleStore<ApplicationRole>(identity);
            var roleManager = new RoleManager<ApplicationRole, string>(roleStore);

            var userId = principal.Identity.GetUserId();

            var roles = roleManager.Roles.Where(x => x.Users.Any(u => u.UserId == userId)).ToList();

            foreach (var role in roles)
            {
                var permissionObject = identity.Permissions.SingleOrDefault(x => x.Name == permission);

                if (permissionObject == null)
                {
                    throw new Exception($"Permission '{permission}' not found");
                }

                var hasPermission = identity.RolePermissions.Any(x =>
                    x.PermissionId == permissionObject.Id && x.RoleId == role.Id);

                if (hasPermission)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckUserType(this IPrincipal principal, UserType userType)
        {
            var identity = new IdentityContext();
            var userStore = new UserStore<ApplicationUser>(identity);
            var userManager = new UserManager<ApplicationUser, string>(userStore);

            var userId = principal.Identity.GetUserId();

            var user = userManager.FindById(userId);

            return user.UserType == userType;
        }

        public static async Task<bool> CheckUserTypeAsync(this IPrincipal principal, UserType userType)
        {
            var principalUserType = await principal.GetUserTypeAsync();
            
            return principalUserType == userType;
        }

        public static async Task<UserType> GetUserTypeAsync(this IPrincipal principal)
        {
            var identity = new IdentityContext();
            var userStore = new UserStore<ApplicationUser>(identity);
            var userManager = new UserManager<ApplicationUser, string>(userStore);

            var userId = principal.Identity.GetUserId();

            var user = await userManager.FindByIdAsync(userId);

            return user.UserType;
        }

        public static UserType GetUserType(this IPrincipal principal)
        {
            var identity = new IdentityContext();
            var userStore = new UserStore<ApplicationUser>(identity);
            var userManager = new UserManager<ApplicationUser, string>(userStore);

            var userId = principal.Identity.GetUserId();

            var user = userManager.FindById(userId);

            return user.UserType;
        }

        public static async Task<bool> HasPermissionAsync(this IPrincipal principal, string permission)
        {
            var identity = new IdentityContext();
            var roleStore = new RoleStore<ApplicationRole>(identity);
            var roleManager = new RoleManager<ApplicationRole, string>(roleStore);

            var userId = principal.Identity.GetUserId();

            var roles = await roleManager.Roles.Where(x => x.Users.Any(u => u.UserId == userId)).ToListAsync();

            foreach (var role in roles)
            {
                var permissionObject = await identity.Permissions.SingleOrDefaultAsync(x => x.Name == permission);

                if (permissionObject == null)
                {
                    throw new Exception($"Permission '{permission}' not found");
                }

                var hasPermission = await identity.RolePermissions.AnyAsync(x =>
                    x.PermissionId == permissionObject.Id && x.RoleId == role.Id);

                if (hasPermission)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ModelIsValid<T>(T model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(model, validationContext, results, true))
            {
                return true;
            }

            ErrorMessages = results.Select(x => x.ErrorMessage).ToList();
            return false;
        }
    }
}