using System.Security.Claims;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Services
{
    public class ActivityService : BaseUserService, IActivityService
    {
        public ActivityService(ISessionUser user) : base(user)
        {
        }
    }
}
