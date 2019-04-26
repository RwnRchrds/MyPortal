using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
using MyPortal.Models;

namespace MyPortal.Helpers
{    
    public static class UserHelper
    {
        private static readonly ApplicationDbContext _identity;

        static UserHelper()
        {
            _identity = new ApplicationDbContext();
        }

        public static ApplicationUser GetApplicationUser(this IPrincipal user)
        {
            var applicationUser = _identity.Users.SingleOrDefault(x => x.Id == user.Identity.GetUserId());

            return applicationUser;
        }

        public static int? GetSelectedAcademicYearId(this IPrincipal user)
        {
            var userId = user.Identity.GetUserId();
            var applicationUser = _identity.Users.SingleOrDefault(x => x.Id == userId);

            return applicationUser?.SelectedAcademicYearId;
        }
    }
}