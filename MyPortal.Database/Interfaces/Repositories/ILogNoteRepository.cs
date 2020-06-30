using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ILogNoteRepository : IReadWriteRepository<LogNote>
    {
        Task<IEnumerable<LogNote>> GetByStudent(Guid studentId, Guid academicYearId);
    }
}
