using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class HouseService : BaseService, IHouseService
    {
        public HouseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<HouseModel>> GetHouses()
        {
            var houses = await UnitOfWork.Houses.GetAll();

            return houses.OrderBy(h => h.Name).Select(BusinessMapper.Map<HouseModel>);
        }
    }
}
