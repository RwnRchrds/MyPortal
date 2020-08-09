using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Microsoft.Extensions.Configuration;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DocumentProvision;
using File = Google.Apis.Drive.v3.Data.File;

namespace MyPortal.Logic.DocumentProviders
{
    public class GoogleDocumentProvider : IDocumentProvider
    {
        private readonly DriveService _driveService;

        public GoogleDocumentProvider(IConfiguration config)
        {
            var googleHelper = new GoogleHelper(config);
            _driveService = new DriveService(googleHelper.GetInitializer());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<FileMetadata> GetMetadata(string fileId)
        {
            var request = _driveService.Files.Get(fileId);

            request.Fields = "id, name, description, iconLink, mimeType, webViewLink";

            var data = await request.ExecuteAsync();

            var metadata = new FileMetadata
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                IconLink = data.IconLink,
                WebViewLink = data.WebViewLink,
                MimeType = data.MimeType
            };

            return metadata;
        }

        public async Task<string> UploadFile(FileUpload upload)
        {
            var file = new File
            {
                Name = upload.Metadata.Name,
                Description = upload.Metadata.Description
            };

            var request = _driveService.Files.Create(file, upload.Stream, upload.Metadata.MimeType);
        }

        public Task DeleteFile(string fileId)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetDownloadStream(string fileId)
        {
            throw new NotImplementedException();
        }
    }
}
