using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.WebSockets;
using Microsoft.AspNet.Identity;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;

namespace MyPortal.Processes
{    
    public static class UserProcesses
    {
        private static readonly IdentityContext _identity;
        static UserProcesses()
        {
           _identity = new IdentityContext(); 
        }

        public static void ChangeSelectedAcademicYear(this IPrincipal user, int academicYearId)
        {
            var applicationUser = user.GetApplicationUser();

            if (applicationUser == null)
            {
                throw new Exception("User not found");
            }

            applicationUser.SelectedAcademicYearId = academicYearId;

            _identity.SaveChanges();
        }

        public static ApplicationUser GetApplicationUser(this IPrincipal user)
        {
            var userId = user.Identity.GetUserId();
            var applicationUser = _identity.Users.SingleOrDefault(x => x.Id == userId);

            return applicationUser;
        }

        public static async Task<int?> GetSelectedAcademicYearId(this IPrincipal user)
        {
            var userId = user.Identity.GetUserId();
            var applicationUser = await _identity.Users.SingleOrDefaultAsync(x => x.Id == userId);

            if (applicationUser == null)
            {
                throw new NotFoundException("User not found");
            }

            return applicationUser.SelectedAcademicYearId;
        }
    }
}