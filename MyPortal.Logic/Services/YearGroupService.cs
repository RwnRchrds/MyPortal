using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class YearGroupService : BaseService, IYearGroupService
    {
        private readonly IYearGroupRepository _yearGroupRepository;

        public YearGroupService(IYearGroupRepository yearGroupRepository)
        {
            _yearGroupRepository = yearGroupRepository;
        }

        public override void Dispose()
        {
            _yearGroupRepository.Dispose();
        }

        public async Task<IEnumerable<YearGroupModel>> GetYearGroups()
        {
            var yearGroups = await _yearGroupRepository.GetAll();

            return yearGroups.Select(BusinessMapper.Map<YearGroupModel>);
        }
    }
}
