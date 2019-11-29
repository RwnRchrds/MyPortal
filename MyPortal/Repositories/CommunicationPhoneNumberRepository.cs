using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CommunicationPhoneNumberRepository : Repository<CommunicationPhoneNumber>, ICommunicationPhoneNumberRepository
    {
        public CommunicationPhoneNumberRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CommunicationPhoneNumber>> GetByPerson(int personId)
        {
            return await Context.CommunicationPhoneNumbers.Where(x => x.PersonId == personId).ToListAsync();
        }
    }
}