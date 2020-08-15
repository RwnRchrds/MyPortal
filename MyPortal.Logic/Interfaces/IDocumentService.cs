using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models.Filters;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Entity;
using File = Google.Apis.Drive.v3.Data.File;

namespace MyPortal.Logic.Interfaces
{
    public interface IDocumentService : IService
    {
        Task Create(params DocumentModel[] documents);
        Task<FileMetadata> GetAttachmentByDocument(Guid documentId);
        Task<Lookup> GetTypes(DocumentTypeFilter filter);
        Task<FileDownload> GetDownloadByDocument(Guid documentId);
        Task<DocumentModel> GetDocumentById(Guid documentId);
        Task Update(params DocumentModel[] documents);
        Task Delete(params Guid[] documentIds);
    }
}
