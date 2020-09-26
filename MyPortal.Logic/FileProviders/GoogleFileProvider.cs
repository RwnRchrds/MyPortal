using System;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Upload;
using Microsoft.Extensions.Configuration;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Requests.Documents;
using File = Google.Apis.Drive.v3.Data.File;

namespace MyPortal.Logic.FileProviders
{
    public class GoogleFileProvider : HostedFileProvider
    {
        private readonly DriveService _driveService;

        public GoogleFileProvider(IConfiguration config)
        {
            var googleHelper = new GoogleHelper(config);
            _driveService = new DriveService(googleHelper.GetInitializer());
        }

        public override void Dispose()
        {
            _driveService.Dispose();
        }

        public override async Task<FileMetadata> FetchMetadata(string fileId, FileMetadata metadata)
        {
            var request = _driveService.Files.Get(fileId);

            request.Fields = "id, name, description, mimeType, webViewLink";

            var data = await request.ExecuteAsync();

            metadata.Id = data.Id;
            metadata.IconLink = data.IconLink;
            metadata.WebViewLink = data.WebViewLink;
            metadata.MimeType = data.MimeType;

            return metadata;
        }

        public override async Task<string> UploadFile(UploadAttachmentModel upload)
        {
            var metadata = new File();

            metadata.Name = upload.File.FileName;

            var request = _driveService.Files.Create(metadata, upload.File.OpenReadStream(), upload.File.ContentType);

            await request.UploadAsync();

            var result = request.ResponseBody;

            return result.Id;
        }

        public override async Task DeleteFile(string fileId)
        {
            var request = _driveService.Files.Delete(fileId);

            await request.ExecuteAsync();
        }

        public override async Task<Stream> DownloadFileToStream(string fileId)
        {
            var stream = new MemoryStream();

            var request = _driveService.Files.Get(fileId);

            await request.DownloadAsync(stream);

            stream.Position = 0;

            return stream;
        }
    }
}
