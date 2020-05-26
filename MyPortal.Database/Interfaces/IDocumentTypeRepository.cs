using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Filters;

namespace MyPortal.Database.Interfaces
{
    public interface IDocumentTypeRepository : IReadRepository<DocumentType>
    {
        Task<IEnumerable<DocumentType>> Get(DocumentTypeFilter filter);
    }
}
