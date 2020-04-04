using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Documents;

namespace MyPortal.Logic.Interfaces
{
    public interface IDocumentService
    {
        Task Create(Guid userId, params DocumentUpload[] documents);
        Task<string> GetUrl(Guid userId, Guid documentId);
        Task<Stream> Download(Guid userId, Guid documentId);
    }
}
