using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IHomeworkSubmissionRepository : IReadWriteRepository<HomeworkSubmission>, IUpdateRepository<HomeworkSubmission>
    {
        Task<IEnumerable<HomeworkSubmission>> GetHomeworkSubmissionsByStudent(Guid studentId);
        Task<IEnumerable<HomeworkSubmission>> GetHomeworkSubmissionsByStudentGroup(Guid studentGroupId);
    }
}
