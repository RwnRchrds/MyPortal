using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Services.Identity
{
    public class UserService : IdentityService
    {
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