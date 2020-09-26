using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.FileProviders
{
    public class LocalFileProvider : IFileProvider
    {
        private readonly string _fileStoragePath;

        public LocalFileProvider(IConfiguration config)
        {
            var installPath = config.GetValue<string>("MyPortal:InstallLocation");

            _fileStoragePath = Path.Combine(installPath, "FileStorage");

            Directory.CreateDirectory(_fileStoragePath);
        }

        public void Dispose()
        {

        }

        public async Task<string> UploadFile(UploadAttachmentModel upload)
        {
            var fileName = Path.GetRandomFileName();

            var path = Path.Combine(_fileStoragePath, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await upload.File.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task DeleteFile(string fileId)
        {
            var filePath = Path.Combine(_fileStoragePath, fileId);

            File.Delete(filePath);
        }

        public async Task<Stream> DownloadFileToStream(string fileId)
        {
            var path = Path.Combine(_fileStoragePath, fileId);

            if (File.Exists(path))
            {
                var stream = File.Open(path, FileMode.Open);

                stream.Position = 0;

                return stream;
            }

            throw new NotFoundException("File not found.");
        }
    }
}
