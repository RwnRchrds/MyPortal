using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class DocumentTypeRepository : ReadRepository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}