using System;
using System.IO;
using System.Threading.Tasks;
using MyPortal.Logic.Models.DocumentProvision;

namespace MyPortal.Logic.Interfaces
{
    public interface IHostedFileProvider : IDisposable
    {
        Task<HostedFileMetadata> FetchMetadata(string fileId);
        Task<Stream> DownloadFileToStream(string fileId);
    }
}
