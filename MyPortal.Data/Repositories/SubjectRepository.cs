using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SubjectRepository : ReadWriteRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}