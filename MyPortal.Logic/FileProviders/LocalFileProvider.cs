using System;
using System.IO;
using System.Linq;
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

        public async Task<Database.Models.Entity.File> SaveFile(FileUploadRequestModel upload)
        {
            var fileId = Guid.NewGuid().ToString("N");

            var fileName = $"{fileId}";

            var path = Path.Combine(_fileStoragePath, fileName);

            byte[] sourceData;
            
            // Encrypt file contents before saving

            using (var ms = new MemoryStream())
            {
                await upload.Attachment.CopyToAsync(ms);
                sourceData = ms.ToArray();
            }
            
            var key = Configuration.Instance.FileEncryptionKey;
            var encryptionResult = await CryptoHelper.EncryptAsync(sourceData, key);

            // Store the file and the IV together
            var file = encryptionResult.Iv.Concat(encryptionResult.Data).ToArray();

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await stream.WriteAsync(file);
            }

            return new Database.Models.Entity.File
            {
                FileName = upload.Attachment.Name,
                ContentType = upload.Attachment.ContentType,
                FileId = fileId
            };
        }

        public void DeleteFile(string fileId)
        {
            var filePath = Path.Combine(_fileStoragePath, fileId);

            File.Delete(filePath);
        }

        public async Task<byte[]> LoadFileData(string fileId)
        {
            var path = Path.Combine(_fileStoragePath, fileId);

            if (File.Exists(path))
            {
                var key = Configuration.Instance.FileEncryptionKey;
                
                var encryptedData = await File.ReadAllBytesAsync(path);

                var iv = encryptedData.Take(16).ToArray();

                var fileData = encryptedData.Skip(16).Take(encryptedData.Length - 16).ToArray();

                var decryptedData = await CryptoHelper.DecryptAsync(fileData, key, iv);

                return decryptedData;
            }

            throw new NotFoundException("File not found.");
        }

        public async Task<Stream> LoadFileAsStream(string fileId)
        {
            var data = await LoadFileData(fileId);

            return new MemoryStream(data);
        }
    }
}
