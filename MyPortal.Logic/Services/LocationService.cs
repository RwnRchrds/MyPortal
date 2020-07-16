using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class LocationService : BaseService, ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        
        public LocationService(ILocationRepository locationRepository) : base("Location")
        {
            _locationRepository = locationRepository;
        }

        public async Task<Lookup> GetLocations()
        {
            var locations = await _locationRepository.GetAll();

            return new Lookup(locations.OrderBy(x => x.Description).ToDictionary(x => x.Description, x => x.Id));
        }

        public override void Dispose()
        {
            _locationRepository.Dispose();
        }
    }
}