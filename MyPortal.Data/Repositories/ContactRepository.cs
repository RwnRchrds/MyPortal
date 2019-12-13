using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ContactRepository : ReadWriteRepository<Contact>, IContactRepository
    {
        public ContactRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}