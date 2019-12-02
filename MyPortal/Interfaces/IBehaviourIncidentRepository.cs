using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IBehaviourIncidentRepository : IReadWriteRepository<BehaviourIncident>
    {
        Task<int> GetCountByStudent(int studentId, int academicYearId);
        Task<int> GetPointsByStudent(int studentId, int academicYearId);
        Task<IEnumerable<BehaviourIncident>> GetByStudent(int studentId, int academicYearId);
        Task<int> GetPointsToday();
    }
}
