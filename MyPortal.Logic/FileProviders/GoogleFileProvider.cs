using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Web;
using File = MyPortal.Database.Models.Entity.File;

namespace MyPortal.Logic.FileProviders
{
    public class GoogleFileProvider : IHostedFileProvider
    {
        private readonly DriveService _driveService;

        public GoogleFileProvider()
        {
            var googleHelper = new GoogleHelper(Configuration.Instance.GoogleConfig);
            _driveService = new DriveService(googleHelper.GetInitializer(scopes: DriveService.Scope.Drive));
        }

        public void Dispose()
        {
            _driveService.Dispose();
        }

        public async Task<IEnumerable<WebAction>> GetWebActions(string fileId)
        {
            var actions = new List<WebAction>();
            
            var request = _driveService.Files.Get(fileId);

            var data = await request.ExecuteAsync();

            request.Fields = "id, name, mimeType, webViewLink";

            var webViewAction = new WebAction("View on Web", data.WebViewLink);

            actions.Add(webViewAction);

            return actions;
        }

        public async Task<File> GetFileById(string fileId)
        {
            var request = _driveService.Files.Get(fileId);

            request.Fields = "id, name, mimeType";

            var data = await request.ExecuteAsync();

            var file = new File
            {
                FileId = data.Id,
                FileName = data.Name,
                ContentType = data.MimeType
            };

            return file;
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
