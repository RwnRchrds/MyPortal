using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.FileProviders
{
    public abstract class HostedFileProvider : IFileProvider
    {
        public abstract Task<FileMetadata> FetchMetadata(string fileId, FileMetadata metadata);
        public abstract void Dispose();
        public abstract Task<string> UploadFile(UploadAttachmentModel upload);
        public abstract Task DeleteFile(string fileId);
        public abstract Task<Stream> DownloadFileToStream(string fileId);
    }
}
