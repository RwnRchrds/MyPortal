﻿using System.Collections.Generic;
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
    public class LocationService : BaseService, ILocationService
    {
        public async Task<IEnumerable<LocationModel>> GetLocations()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var locations = await unitOfWork.Locations.GetAll();

                return locations.Select(l => new LocationModel(l));
            }
        }

        public LocationService(ClaimsPrincipal user) : base(user)
        {
        }
    }
}