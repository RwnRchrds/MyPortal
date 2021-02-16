using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Filters;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IDocumentService : IService
    {
        Task Create(params DocumentModel[] documents);
        Task Update(params UpdateDocumentModel[] documents);
        Task<IEnumerable<DocumentTypeModel>> GetTypes(DocumentTypeFilter filter);
        Task<DocumentModel> GetDocumentById(Guid documentId);
        Task Delete(params Guid[] documentIds);
    }
}
