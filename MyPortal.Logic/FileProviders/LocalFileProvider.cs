using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.FileProviders
{
    public class LocalFileProvider : ILocalFileProvider
    {
        private readonly string _fileStoragePath;

        public LocalFileProvider(IConfiguration config)
        {
            var installPath = config.GetValue<string>("MyPortal:InstallLocation");

            _fileStoragePath = Path.Combine(installPath, "FileStorage");

            Directory.CreateDirectory(_fileStoragePath);
        }

        public async Task<string> UploadFile(UploadAttachmentModel upload)
        {
            var fileName = Guid.NewGuid().ToString("N");

            var path = Path.Combine(_fileStoragePath, fileName);

            byte[] encryptedData;

            using (var ms = new MemoryStream())
            {
                await upload.File.CopyToAsync(ms);
                var sourceData = ms.ToArray();
                encryptedData = Encryption.Encrypt(sourceData, fileName);
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await stream.WriteAsync(encryptedData);
            }

            return fileName;
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
