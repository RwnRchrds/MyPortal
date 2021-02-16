using System.IO;
using System.Threading.Tasks;
using MyPortal.Logic.Models.DocumentProvision;

namespace MyPortal.Logic.Interfaces
{
    public interface IHostedFileProvider
    {
        Task<HostedFileMetadata> FetchMetadata(string fileId);
        Task<Stream> DownloadFileToStream(string fileId);
    }
}
