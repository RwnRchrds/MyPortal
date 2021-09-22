using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamSpecialArrangementRepository : BaseReadWriteRepository<ExamSpecialArrangement>, IExamSpecialArrangementRepository
    {
        public ExamSpecialArrangementRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(ExamSpecialArrangement entity)
        {
            var arrangement = await Context.ExamSpecialArrangements.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (arrangement == null)
            {
                throw new EntityNotFoundException("Special arrangement not found.");
            }

            arrangement.Description = entity.Description;
        }
    }
}