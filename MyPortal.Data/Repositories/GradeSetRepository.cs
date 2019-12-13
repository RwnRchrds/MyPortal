using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class GradeSetRepository : ReadWriteRepository<GradeSet>, IGradeSetRepository
    {
        public GradeSetRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}