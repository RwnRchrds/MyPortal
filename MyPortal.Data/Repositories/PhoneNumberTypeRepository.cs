using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class PhoneNumberTypeRepository : ReadRepository<PhoneNumberType>, IPhoneNumberTypeRepository
    {
        public PhoneNumberTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}