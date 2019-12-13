using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IProfileLogNoteRepository : IReadWriteRepository<ProfileLogNote>
    {
        Task<IEnumerable<ProfileLogNote>> GetByStudent(int studentId, int academicYearId);
    }
}
