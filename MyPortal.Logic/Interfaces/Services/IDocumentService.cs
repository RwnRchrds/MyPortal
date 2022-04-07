using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Filters;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using MyPortal.Logic.Models.Response.Documents;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IDocumentService
    {
        Task CreateDocument(Guid userId, params CreateDocumentModel[] documents);
        Task UpdateDocument(params UpdateDocumentModel[] documents);
        Task<IEnumerable<DocumentTypeModel>> GetTypes(DocumentTypeFilter filter);
        Task<DocumentModel> GetDocumentById(Guid documentId);
        Task DeleteDocument(params Guid[] documentIds);
        Task<DirectoryChildren> GetDirectoryChildren(Guid directoryId, bool includeRestricted);
        Task<DirectoryModel> GetDirectoryById(Guid directoryId);
        Task CreateDirectory(params CreateDirectoryModel[] directories);
        Task UpdateDirectory(params UpdateDirectoryModel[] directories);
        Task DeleteDirectory(params Guid[] directoryIds);
        Task<bool> DirectoryIsPrivate(Guid directoryId);
        Task<bool> DirectoryIsPublic(Guid directoryId);
        Task<bool> DirectoryIsRestricted(Guid directoryId);
    }
}
