using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;


namespace MyPortal.Logic.Services
{
    public abstract class BaseUserService : BaseService
    {
        protected ICurrentUser User;
        
        public BaseUserService(ICurrentUser user)
        {
            User = user;
        }
    }
}
