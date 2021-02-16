using System;
using System.IO;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.Interfaces
{
    public interface ILocalFileProvider
    {
        Task<string> UploadFile(UploadAttachmentModel upload);
        void DeleteFile(string fileId);
        Task<Stream> DownloadFileToStream(string fileId);
    }
}
