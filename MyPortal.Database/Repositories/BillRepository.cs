using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class BillRepository : BaseReadWriteRepository<Bill>, IBillRepository
    {
        public BillRepository(ApplicationDbContext context) : base(context, "Bill")
        {
            
        }
    }
}