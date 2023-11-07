using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ICurriculumBandRepository : IReadWriteRepository<CurriculumBand>, IUpdateRepository<CurriculumBand>
    {
        Task<IEnumerable<CurriculumBand>> GetCurriculumBandsByYearGroup(Guid yearGroupId);
    }
}