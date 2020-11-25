using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ICurriculumBlockRepository : IReadWriteRepository<CurriculumBlock>
    {
        Task<IEnumerable<CurriculumBlock>> GetByCurriculumBand(Guid bandId);
        Task<Guid?> GetAcademicYearId(Guid blockId);
        Task<bool> CheckUniqueCode(Guid academicYearId, string code);
    }
}