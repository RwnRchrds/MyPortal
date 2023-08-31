using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamQualificationRepository : BaseReadWriteRepository<ExamQualification>, IExamQualificationRepository
    {
        public ExamQualificationRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(ExamQualification entity)
        {
            var qualification = await DbUser.Context.ExamQualifications.FirstOrDefaultAsync(x => x.Id == entity.Id);

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