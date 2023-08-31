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
    public class GradeSetRepository : BaseReadWriteRepository<GradeSet>, IGradeSetRepository
    {
        public GradeSetRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(GradeSet entity)
        {
            var gradeSet = await DbUser.Context.GradeSets.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (gradeSet == null)
            {
                throw new EntityNotFoundException("Grade set not found.");
            }

            if (gradeSet.System)
            {
                throw ExceptionHelper.UpdateSystemEntityException;
            }

            gradeSet.Name = entity.Name;
            gradeSet.Description = entity.Description;
            gradeSet.Active = entity.Active;
        }
    }
}