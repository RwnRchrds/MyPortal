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
        public void Validate<T>(T model)
        {
            ValidationHelper.ValidateModel(model);
        }
    }
}
