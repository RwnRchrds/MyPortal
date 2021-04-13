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
    public class HouseService : BaseService, IHouseService
    {
        public async Task<IEnumerable<HouseModel>> GetHouses()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var houses = await unitOfWork.Houses.GetAll();

                // TODO: Add order by
                return houses.Select(BusinessMapper.Map<HouseModel>);
            }
        }
    }
}
