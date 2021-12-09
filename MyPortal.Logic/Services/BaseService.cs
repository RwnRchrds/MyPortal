using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public abstract class BaseService
    {
        protected ClaimsPrincipal User;

        public BaseService(ClaimsPrincipal user)
        {
            User = user;
        }

        public async Task<UserModel> GetCurrentUser()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var userId = User.GetUserId();

                var user = await unitOfWork.Users.GetById(userId);

                return new UserModel(user);
            }
        }
    }
}
