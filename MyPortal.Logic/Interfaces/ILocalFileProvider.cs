using System;
using System.IO;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Documents;
using File = MyPortal.Database.Models.Entity.File;

namespace MyPortal.Logic.Interfaces
{
    public interface ILocalFileProvider
    {
        Task<File> SaveFile(UploadAttachmentRequestModel upload);
        void DeleteFile(string fileId);
        Task<Stream> DownloadFileToStream(string fileId);
    }
}
