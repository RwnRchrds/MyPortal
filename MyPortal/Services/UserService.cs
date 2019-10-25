using System.Data.Entity;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MyPortal.Exceptions;
using MyPortal.Models;

namespace MyPortal.Services
{    
    public static class UserService
    {
        private static readonly IdentityContext _identity;
        static UserService()
        {
           _identity = new IdentityContext(); 
        }

        public static async Task ChangeSelectedAcademicYear(this IPrincipal user, int academicYearId)
        {
            var applicationUser = await user.GetApplicationUser();

            if (applicationUser == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "User not found");
            }

            applicationUser.SelectedAcademicYearId = academicYearId;

            await _identity.SaveChangesAsync();
        }

        public static async Task<UserType> GetUserType(this IPrincipal user)
        {
            var applicationUser = await user.GetApplicationUser();

            return applicationUser.UserType;
        }

        public static async Task<ApplicationUser> GetApplicationUser(this IPrincipal user)
        {
            var userId = user.Identity.GetUserId();
            var applicationUser = await _identity.Users.SingleOrDefaultAsync(x => x.Id == userId);

            return applicationUser;
        }

        public static async Task<int?> GetSelectedAcademicYearId(this IPrincipal user)
        {
            var userId = user.Identity.GetUserId();
            var applicationUser = await _identity.Users.SingleOrDefaultAsync(x => x.Id == userId);

            if (applicationUser == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "User not found");
            }

            return applicationUser.SelectedAcademicYearId;
        }
    }
}