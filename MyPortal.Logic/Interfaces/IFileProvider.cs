using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.Interfaces
{
    public interface IFileProvider : IDisposable
    {
        Task<string> UploadFile(UploadAttachmentModel upload);
        Task DeleteFile(string fileId);
        Task<Stream> DownloadFileToStream(string fileId);
    }
}
