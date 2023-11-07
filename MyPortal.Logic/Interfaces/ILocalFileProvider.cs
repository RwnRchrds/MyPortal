using System.IO;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Documents;
using File = MyPortal.Database.Models.Entity.File;

namespace MyPortal.Logic.Interfaces
{
    public interface ILocalFileProvider
    {
        Task<File> SaveFile(FileUploadRequestModel upload);
        void DeleteFile(string fileId);
        Task<byte[]> LoadFileData(string fileId);
        Task<Stream> LoadFileAsStream(string fileId);
    }
}