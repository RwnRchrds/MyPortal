using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamQualificationRepository : BaseReadWriteRepository<ExamQualification>, IExamQualificationRepository
    {
        public ExamQualificationRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(ExamQualification entity)
        {
            var qualification = await Context.ExamQualifications.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (qualification == null)
            {
                throw new EntityNotFoundException("Qualification not found.");
            }

            if (qualification.System)
            {
                throw ExceptionHelper.UpdateSystemEntityException;
            }

            qualification.JcQualificationCode = entity.JcQualificationCode;
        }
    }
}