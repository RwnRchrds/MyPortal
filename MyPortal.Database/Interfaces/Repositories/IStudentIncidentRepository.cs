using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories;

public interface IStudentIncidentRepository : IReadWriteRepository<StudentIncident>, IUpdateRepository<StudentIncident>
{
    Task<IEnumerable<StudentIncident>> GetByStudent(Guid studentId, Guid academicYearId);
    Task<IEnumerable<StudentIncident>> GetByIncident(Guid incidentId);
    Task<int> GetCountByStudent(Guid studentId, Guid academicYearId);
    Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId);
    Task<int> GetCountByIncident(Guid incidentId);
}