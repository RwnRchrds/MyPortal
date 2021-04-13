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
    public class TrainingCourseRepository : BaseReadWriteRepository<TrainingCourse>, ITrainingCourseRepository
    {
        public TrainingCourseRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(TrainingCourse entity)
        {
            var course = await Context.TrainingCourses.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (course == null)
            {
                throw new EntityNotFoundException("Training course not found.");
            }

            course.Name = entity.Name;
            course.Code = entity.Code;
        }
    }
}