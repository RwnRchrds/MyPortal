using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class LocationService : BaseService, ILocationService
    {
        public LocationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<LocationModel>> GetLocations()
        {
            var locations = await UnitOfWork.Locations.GetAll();

            return locations.Select(BusinessMapper.Map<LocationModel>);
        }
    }
}