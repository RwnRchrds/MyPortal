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
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Document>> GetAllDocuments()
        {
            return await Context.Documents.Where(x => x.IsGeneral).ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetApprovedDocuments()
        {
            return await Context.Documents.Where(x => x.IsGeneral && x.Approved).ToListAsync();
        }
    }
}