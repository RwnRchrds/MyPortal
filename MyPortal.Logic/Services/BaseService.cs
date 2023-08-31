using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;


namespace MyPortal.Logic.Services
{
    public abstract class BaseService
    {
        protected readonly ISessionUser User;

        public BaseService(ISessionUser user) : base()
        {
            User = user;
        }
        
        public UnauthorisedException Unauthenticated()
        {
            return new UnauthorisedException("The user is not authenticated.");
        }
        
        public void Validate<T>(T model)
        {
            ValidationHelper.ValidateModel(model);
        }
    }
}
