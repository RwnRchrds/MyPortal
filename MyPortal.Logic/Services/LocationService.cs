using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class LocationService : BaseService, ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        
        public LocationService(ApplicationDbContext context)
        {
            _locationRepository = new LocationRepository(context);
        }

        public async Task<IEnumerable<LocationModel>> GetLocations()
        {
            var locations = await _locationRepository.GetAll();

            return locations.Select(BusinessMapper.Map<LocationModel>);
        }

        public override void Dispose()
        {
            _locationRepository.Dispose();
        }
    }
}