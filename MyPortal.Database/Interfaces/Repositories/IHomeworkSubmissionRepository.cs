using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IHomeworkSubmissionRepository : IReadWriteRepository<HomeworkSubmission>,
        IUpdateRepository<HomeworkSubmission>
    {
        Task<IEnumerable<HomeworkSubmission>> GetHomeworkSubmissionsByStudent(Guid studentId);
        Task<IEnumerable<HomeworkSubmission>> GetHomeworkSubmissionsByStudentGroup(Guid studentGroupId);
    }
}