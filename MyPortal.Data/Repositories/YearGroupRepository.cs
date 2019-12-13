using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class YearGroupRepository : ReadWriteRepository<YearGroup>, IYearGroupRepository
    {
        public YearGroupRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}