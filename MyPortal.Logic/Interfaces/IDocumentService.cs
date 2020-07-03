using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models.Filters;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Google;
using MyPortal.Logic.Models.Requests.Documents;
using File = Google.Apis.Drive.v3.Data.File;

namespace MyPortal.Logic.Interfaces
{
    public interface IDocumentService : IService
    {
        Task Create(params DocumentModel[] documents);
        Task<File> GetFileById(Guid documentId);
        Task<Lookup> GetTypes(DocumentTypeFilter filter);
        Task<FileDownload> GetDownloadById(Guid documentId, bool downloadAsPdf = false);
        Task<DocumentModel> GetDocumentById(Guid documentId);
        Task Update(params DocumentModel[] documents);
        Task Delete(params Guid[] documentIds);
    }
}
