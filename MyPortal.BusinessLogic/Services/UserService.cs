using System.Threading.Tasks;
using MyPortal.Data.Interfaces;

namespace MyPortal.BusinessLogic.Services
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