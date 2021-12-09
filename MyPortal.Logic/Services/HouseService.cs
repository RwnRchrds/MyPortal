using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                
                var houseModels = houses.Select(h => new HouseModel(h)).ToList();

                return houseModels.OrderBy(h => h.StudentGroup.Description).ToList();
            }
        }

        public HouseService(ClaimsPrincipal user) : base(user)
        {
        }
    }
}
