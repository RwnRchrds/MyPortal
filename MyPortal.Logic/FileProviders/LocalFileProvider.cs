using System;
using System.IO;
using System.Threading.Tasks;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.FileProviders
{
    public class LocalFileProvider : ILocalFileProvider
    {
        private readonly string _fileStoragePath;

        public LocalFileProvider()
        {
            var installPath = Configuration.Instance.InstallLocation;

            if (string.IsNullOrWhiteSpace(installPath))
            {
                throw new ConfigurationException(@"MyPortal path has not been set.");
            }

            _fileStoragePath = Path.Combine(installPath, "FileStorage");

            Directory.CreateDirectory(_fileStoragePath);
        }

        public async Task<Database.Models.Entity.File> SaveFile(UploadAttachmentModel upload)
        {
            var fileId = Guid.NewGuid().ToString("N");

            var path = Path.Combine(_fileStoragePath, fileId);

            byte[] encryptedData;
            
            // Encrypt file contents before saving

            using (var ms = new MemoryStream())
            {
                await upload.File.CopyToAsync(ms);
                var sourceData = ms.ToArray();
                encryptedData = Encryption.Encrypt(sourceData, fileId);
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await stream.WriteAsync(encryptedData);
            }

            return new Database.Models.Entity.File
            {
                FileName = upload.File.Name,
                ContentType = upload.File.ContentType,
                FileId = fileId
            };
        }

        public void DeleteFile(string fileId)
        {
            var filePath = Path.Combine(_fileStoragePath, fileId);

            File.Delete(filePath);
        }

        public async Task<Stream> DownloadFileToStream(string fileId)
        {
            var path = Path.Combine(_fileStoragePath, fileId);

            if (File.Exists(path))
            {
                var encryptedData = await File.ReadAllBytesAsync(path);

                var decryptedData = Encryption.Decrypt(encryptedData, fileId);

                var ms = new MemoryStream();

                await ms.WriteAsync(decryptedData);

                return ms;
            }

            throw new NotFoundException("File not found.");
        }
    }
}
