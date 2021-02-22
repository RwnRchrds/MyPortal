using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class YearGroupService : BaseService, IYearGroupService
    {
        public YearGroupService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<YearGroupModel>> GetYearGroups()
        {
            var yearGroups = await UnitOfWork.YearGroups.GetAll();

            return yearGroups.OrderBy(y => y.Name).Select(BusinessMapper.Map<YearGroupModel>);
        }
    }
}
