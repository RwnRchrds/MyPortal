using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyPortal.ViewModels
{
    public class UserDetailsViewModel
    {
        public IdentityUser User { get; set; }
        public IList<string> UserRoles { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}