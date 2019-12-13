using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SenEventRepository : ReadWriteRepository<SenEvent>, ISenEventRepository
    {
        public SenEventRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}