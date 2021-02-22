using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class RegGroupService : BaseService, IRegGroupService
    {
        public RegGroupService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<RegGroupModel>> GetRegGroups()
        {
            var regGroups = await UnitOfWork.RegGroups.GetAll();

            return regGroups.OrderBy(r => r.Name).Select(BusinessMapper.Map<RegGroupModel>);
        }
    }
}
