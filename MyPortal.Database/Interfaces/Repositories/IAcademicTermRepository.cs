using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAcademicTermRepository : IReadWriteRepository<AcademicTerm>, IUpdateRepository<AcademicTerm>
    {
        Task<IEnumerable<AcademicTerm>> GetByAcademicYear(Guid academicYearId);
    }
}