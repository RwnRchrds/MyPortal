using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class TrainingCourseRepository : ReadWriteRepository<TrainingCourse>, ITrainingCourseRepository
    {
        public TrainingCourseRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}