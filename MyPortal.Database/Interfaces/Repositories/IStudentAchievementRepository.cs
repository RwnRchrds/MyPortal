using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories;

public interface IStudentAchievementRepository : IReadWriteRepository<StudentAchievement>,
    IUpdateRepository<StudentAchievement>
{
    Task<int> GetCountByStudent(Guid studentId, Guid academicYearId);
    Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId);
    Task<IEnumerable<StudentAchievement>> GetByStudent(Guid studentId, Guid academicYearId);
    Task<int> GetPointsToday();
}