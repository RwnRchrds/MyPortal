using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface ICurriculumEnrolmentRepository : IReadWriteRepository<CurriculumEnrolment>
    {
        Task<IEnumerable<CurriculumEnrolment>> GetByClass(int classId);
        Task<IEnumerable<CurriculumEnrolment>> GetByStudent(int studentId);
    }
}
