using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class StudentContactRepository : ReadWriteRepository<StudentContact>, IStudentContactRepository
    {
        public StudentContactRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}