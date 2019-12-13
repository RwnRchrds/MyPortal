using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class PersonAttachmentRepository : ReadWriteRepository<PersonAttachment>, IPersonAttachmentRepository
    {
        public PersonAttachmentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<PersonAttachment>> GetByPerson(int personId)
        {
            return await Context.PersonAttachments.Where(x => x.PersonId == personId).ToListAsync();
        }
    }
}