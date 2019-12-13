using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SenProvisionRepository : ReadWriteRepository<SenProvision>, ISenProvisionRepository
    {
        public SenProvisionRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}