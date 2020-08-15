using System;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Microsoft.Extensions.Configuration;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Documents;

namespace MyPortal.Logic.FileProviders
{
    public class GoogleFileProvider : IFileProvider
    {
        private readonly DriveService _driveService;

        public GoogleFileProvider(IConfiguration config)
        {
            var googleHelper = new GoogleHelper(config);
            _driveService = new DriveService(googleHelper.GetInitializer());
        }

        public void Dispose()
        {
            _driveService.Dispose();
        }

        public async Task<FileMetadata> FetchMetadata(string fileId, FileMetadata metadata)
        {
            var request = _driveService.Files.Get(fileId);

            request.Fields = "id, name, description, iconLink, mimeType, webViewLink";

            var data = await request.ExecuteAsync();

            metadata.Id = data.Id;
            metadata.IconLink = data.IconLink;
            metadata.WebViewLink = data.WebViewLink;
            metadata.MimeType = data.MimeType;

            return metadata;
        }

        public async Task<string> UploadFile(UploadAttachmentModel upload)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteFile(string fileId)
        {
            var request = _driveService.Files.Delete(fileId);

            await request.ExecuteAsync();
        }

        public async Task<Stream> DownloadFileToStream(string fileId)
        {
            var stream = new MemoryStream();

            var request = _driveService.Files.Get(fileId);

            await request.DownloadAsync(stream);

            stream.Position = 0;

            return stream;
        }
    }
}
