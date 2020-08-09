using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyPortal.Logic.Models.DocumentProvision;

namespace MyPortal.Logic.Interfaces
{
    public interface IDocumentProvider : IDisposable
    {
        Task<FileMetadata> GetMetadata(string fileId);
        Task<string> UploadFile(FileUpload upload);
        Task DeleteFile(string fileId);
        Task<Stream> GetDownloadStream(string fileId);
    }
}
