using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SenStatusRepository : ReadRepository<SenStatus>, ISenStatusRepository
    {
        public SenStatusRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}