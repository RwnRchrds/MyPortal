using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class MedicalEventRepository : ReadWriteRepository<MedicalEvent>, IMedicalEventRepository
    {
        public MedicalEventRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}