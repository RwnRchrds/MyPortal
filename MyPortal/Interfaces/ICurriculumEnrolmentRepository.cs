using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface ICurriculumEnrolmentRepository : IRepository<CurriculumEnrolment>
    {
        Task<IEnumerable<CurriculumEnrolment>> GetEnrolmentsByClass(int classId);
        Task<IEnumerable<CurriculumEnrolment>> GetEnrolmentsByStudent(int studentId);
    }
}
