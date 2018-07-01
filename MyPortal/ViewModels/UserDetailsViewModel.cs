using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class UserDetailsViewModel
    {
        public IdentityUser User { get; set; }
        public IList<string> UserRoles { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public ChangePasswordModel ChangePassword { get; set; }
        public UserRoleModel ChangeRole { get; set; }
    }
}