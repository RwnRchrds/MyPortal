using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SenEventTypeRepository : ReadRepository<SenEventType>, ISenEventTypeRepository
    {
        public SenEventTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}