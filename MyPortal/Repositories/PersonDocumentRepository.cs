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
    public class PersonDocumentRepository : ReadWriteRepository<PersonDocument>, IPersonDocumentRepository
    {
        public PersonDocumentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<PersonDocument>> GetByPerson(int personId)
        {
            return await Context.PersonDocuments.Where(x => x.PersonId == personId).ToListAsync();
        }
    }
}