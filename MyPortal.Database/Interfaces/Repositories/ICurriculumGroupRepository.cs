using System;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ICurriculumGroupRepository : IReadWriteRepository<CurriculumGroup>
    {
        Task<bool> CheckUniqueCode(Guid academicYearId, string code);
    }
}