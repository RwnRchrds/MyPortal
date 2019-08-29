using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Models;

namespace MyPortal.Processes
{
    public static class IdentityProcesses
    {
        static readonly IdentityContext _identity;
        static readonly UserManager<ApplicationUser> _userManager;
        static readonly UserStore<ApplicationUser> _userStore;

        static IdentityProcesses()
        {
            _identity = new IdentityContext();
            _userStore = new UserStore<ApplicationUser>(_identity);
            _userManager = new UserManager<ApplicationUser>(_userStore);
        }

        public static bool UserHasPermission(IPrincipal principal, string permission)
        {
            var roles = _userManager.GetRoles(principal.Identity.GetUserId());

            foreach (var role in roles)
            {
                var permissionObject = _identity.Permissions.SingleOrDefault(x => x.ShortName == permission);
                var roleObject = _identity.Roles.SingleOrDefault(x => x.Name == role);

                if (permissionObject == null || roleObject == null)
                {
                    return false;
                }

                var hasPermission = _identity.RolePermissions.Any(x =>
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