using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class CommunicationTypeRepository : ReadRepository<CommunicationType>, ICommunicationTypeRepository
    {
        public CommunicationTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}