using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamSessionRepository : BaseReadWriteRepository<ExamSession>, IExamSessionRepository
    {
        public ExamSessionRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(ExamSession entity)
        {
            var session = await DbUser.Context.ExamSessions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (session == null)
            {
                throw new EntityNotFoundException("Exam session not found.");
            }

            session.StartTime = entity.StartTime;
        }
    }
}