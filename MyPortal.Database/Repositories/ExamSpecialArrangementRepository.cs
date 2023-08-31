using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamSpecialArrangementRepository : BaseReadWriteRepository<ExamSpecialArrangement>,
        IExamSpecialArrangementRepository
    {
        public ExamSpecialArrangementRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(ExamSpecialArrangement entity)
        {
            var arrangement = await DbUser.Context.ExamSpecialArrangements.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (arrangement == null)
            {
                throw new EntityNotFoundException("Special arrangement not found.");
            }

            arrangement.Description = entity.Description;
        }
    }
}