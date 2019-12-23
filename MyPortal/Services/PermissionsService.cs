using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.BusinessLogic.Services;
using MyPortal.Models.Identity;

namespace MyPortal.Services
{
    public static class PermissionsService
    {
        public static bool HasPermission(this IPrincipal principal, string permissionName)
        {
            var identity = new IdentityContext();
            var roleStore = new RoleStore<ApplicationRole>(identity);
            var roleManager = new RoleManager<ApplicationRole, string>(roleStore);

            var userId = principal.Identity.GetUserId();

            var roles = roleManager.Roles.Where(x => x.Users.Any(u => u.UserId == userId)).ToList();

            foreach (var role in roles)
            {
                var permission = identity.Permissions.SingleOrDefault(x => x.Name == permissionName);

                if (permission == null)
                {
                    throw new Exception($"Permission '{permissionName}' not found.");
                }

                var hasPermission = identity.RolePermissions.Any(x =>
                    x.PermissionId == permission.Id && x.RoleId == role.Id);

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

        public static async Task<int> GetSelectedOrCurrentAcademicYearId(this IPrincipal principal)
        {
            var identity = new IdentityContext();
            var userStore = new UserStore<ApplicationUser>(identity);
            var userManager = new UserManager<ApplicationUser, string>(userStore);

            var curriculumService = new CurriculumService();

            var userId = principal.Identity.GetUserId();

            var user = userManager.FindById(userId);

            return user.SelectedAcademicYearId ?? await curriculumService.GetCurrentAcademicYearId();
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
    }
}