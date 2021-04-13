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
    public class RegGroupService : BaseService, IRegGroupService
    {
        public async Task<IEnumerable<RegGroupModel>> GetRegGroups()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var regGroups = await unitOfWork.RegGroups.GetAll();

                // TODO: Add order by
                return regGroups.Select(BusinessMapper.Map<RegGroupModel>);
            }
        }
    }
}
