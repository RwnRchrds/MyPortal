using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Filters;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDocumentTypeRepository : IReadWriteRepository<DocumentType>, IUpdateRepository<DocumentType>
    {
        Task<IEnumerable<DocumentType>> Get(DocumentTypeFilter filter);
    }
}
