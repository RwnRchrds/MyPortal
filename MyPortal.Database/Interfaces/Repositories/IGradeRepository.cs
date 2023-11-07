using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IGradeRepository : IReadWriteRepository<Grade>, IUpdateRepository<Grade>
    {
        Task<IEnumerable<Grade>> GetByGradeSet(Guid gradeSetId);
    }
}