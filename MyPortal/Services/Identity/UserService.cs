using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.BusinessLogic.Services;

namespace MyPortal.Services.Identity
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