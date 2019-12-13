using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IEnrolmentRepository : IReadWriteRepository<Enrolment>
    {
        Task<IEnumerable<Enrolment>> GetByClass(int classId);
        Task<IEnumerable<Enrolment>> GetByStudent(int studentId);
    }
}
