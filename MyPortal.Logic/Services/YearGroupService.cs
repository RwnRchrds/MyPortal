using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class YearGroupService : BaseService, IYearGroupService
    {
        public async Task<IEnumerable<YearGroupModel>> GetYearGroups()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var yearGroups = await unitOfWork.YearGroups.GetAll();

                return yearGroups.OrderBy(y => y.Name).Select(BusinessMapper.Map<YearGroupModel>);
            }
        }
    }
}
