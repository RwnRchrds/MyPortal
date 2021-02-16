using System.IO;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Microsoft.Extensions.Configuration;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DocumentProvision;

namespace MyPortal.Logic.FileProviders
{
    public class GoogleFileProvider : IHostedFileProvider
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

        public async Task<HostedFileMetadata> FetchMetadata(string fileId)
        {
            var hostedMetadata = new HostedFileMetadata();
            
            var request = _driveService.Files.Get(fileId);

            request.Fields = "id, name, description, mimeType, webViewLink";

            var data = await request.ExecuteAsync();

            hostedMetadata.Id = data.Id;
            hostedMetadata.IconLink = data.IconLink;
            hostedMetadata.WebViewLink = data.WebViewLink;
            hostedMetadata.MimeType = data.MimeType;

            return hostedMetadata;
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
