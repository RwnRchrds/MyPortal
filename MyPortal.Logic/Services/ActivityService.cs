using System.Security.Claims;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Services
{
    public class ActivityService : BaseService, IActivityService
    {
        public ActivityService(ClaimsPrincipal user) : base(user)
        {
        }
    }
}
