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
        
        public static bool HasPermission(this IPrincipal principal, string permission)
        {
            var identity = new IdentityContext();
            var userStore =
                new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole,
                    IdentityUserClaim>(identity);
            var userManager = new UserManager<ApplicationUser, string>(userStore);

            var roles = userManager.GetRoles(principal.Identity.GetUserId());

            foreach (var role in roles)
            {
                var permissionObject = identity.Permissions.SingleOrDefault(x => x.Name == permission);
                var roleObject = identity.Roles.SingleOrDefault(x => x.Name == role);

                if (permissionObject == null || roleObject == null)
                {
                    return false;
                }

                var hasPermission = identity.RolePermissions.Any(x =>
                    x.PermissionId == permissionObject.Id && x.RoleId == roleObject.Id);

                if (hasPermission)
                {
                    return true;
                }
            }

            return false;
        }
    }
}