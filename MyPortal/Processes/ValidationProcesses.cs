using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Models;

namespace MyPortal.Processes
{
    public static class ValidationProcesses
    {
        public static List<string> ErrorMessages = new List<string>();

        public static async Task<bool> HasPermission(this IPrincipal principal, string permission)
        {
            var identity = new IdentityContext();
            var roleStore = new RoleStore<ApplicationRole>(identity);
            var roleManager = new RoleManager<ApplicationRole, string>(roleStore);

            var userId = principal.Identity.GetUserId();

            var roles = roleManager.Roles.Where(x => x.Users.Any(u => u.UserId == userId)).ToList();

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