using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class GradeRepository : ReadWriteRepository<Grade>, IGradeRepository
    {
        public GradeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}