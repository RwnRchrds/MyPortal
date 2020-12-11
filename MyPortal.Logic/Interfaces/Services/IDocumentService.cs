using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Filters;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IDocumentService : IService
    {
        Task Create(params DocumentModel[] documents);
        Task<FileMetadata> GetFileMetadataByDocument(Guid documentId);
        Task<IEnumerable<DocumentTypeModel>> GetTypes(DocumentTypeFilter filter);
        Task<FileDownload> GetDownloadByDocument(Guid documentId);
        Task<DocumentModel> GetDocumentById(Guid documentId);
        Task Update(params DocumentModel[] documents);
        Task Delete(params Guid[] documentIds);
    }
}
