using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class HouseService : BaseService, IHouseService
    {
        private readonly IHouseRepository _houseRepository;

        public HouseService(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public override void Dispose()
        {
            _houseRepository.Dispose();
        }

        public async Task<IEnumerable<HouseModel>> GetHouses()
        {
            var houses = await _houseRepository.GetAll();

            return houses.Select(BusinessMapper.Map<HouseModel>);
        }
    }
}
