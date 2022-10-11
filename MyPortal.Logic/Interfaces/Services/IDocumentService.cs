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
        Task CreateDocument(DocumentRequestModel document);
        Task UpdateDocument(Guid documentId, DocumentRequestModel document);
        Task<IEnumerable<DocumentTypeModel>> GetTypes(DocumentTypeFilter filter);
        Task<DocumentModel> GetDocumentById(Guid documentId);
        Task DeleteDocument(Guid documentId);
        Task<DirectoryChildrenModel> GetDirectoryChildren(Guid directoryId, bool includeRestricted);
        Task<DirectoryModel> GetDirectoryById(Guid directoryId);
        Task CreateDirectory(DirectoryRequestModel directory);
        Task UpdateDirectory(Guid directoryId, DirectoryRequestModel directory);
        Task DeleteDirectory(Guid directoryId);
        Task<bool> IsPrivateDirectory(Guid directoryId);
        Task<bool> IsSchoolDirectory(Guid directoryId);
    }
}
