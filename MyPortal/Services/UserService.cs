using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models;

namespace MyPortal.Services
{    
    public class UserService : IdentityService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public UserService() : base()
        {

        }
        
        public async Task ChangeSelectedAcademicYear(string userId, int academicYearId)
        {
            var user = await UserManager.FindByIdAsync(userId);

            user.SelectedAcademicYearId = academicYearId;

            await UserManager.UpdateAsync(user);
        }
    }
}