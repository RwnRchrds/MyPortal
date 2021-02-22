using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Services
{
    public class ActivityService : BaseService, IActivityService
    {
        public ActivityService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
