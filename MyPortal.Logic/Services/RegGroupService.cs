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
    public class RegGroupService : BaseService, IRegGroupService
    {
        private readonly IRegGroupRepository _regGroupRepository;

        public RegGroupService(IRegGroupRepository regGroupRepository)
        {
            _regGroupRepository = regGroupRepository;
        }

        public override void Dispose()
        {
            _regGroupRepository.Dispose();
        }

        public async Task<IEnumerable<RegGroupModel>> GetRegGroups()
        {
            var regGroups = await _regGroupRepository.GetAll();

            return regGroups.OrderBy(r => r.Name).Select(BusinessMapper.Map<RegGroupModel>);
        }
    }
}
