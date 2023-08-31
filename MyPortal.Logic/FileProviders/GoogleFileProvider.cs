using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Web;
using File = MyPortal.Database.Models.Entity.File;

namespace MyPortal.Logic.FileProviders
{
    public class GoogleFileProvider : IHostedFileProvider
    {
        private readonly string _accessToken;

        public GoogleFileProvider(string accessToken)
        {
            _accessToken = accessToken;
        }
        
        private DriveService CreateDriveService()
        {
            var initializer = GoogleHelper.GetInitializer(_accessToken, DriveService.Scope.Drive);
            return new DriveService(initializer);
        }

        public async Task<IEnumerable<WebAction>> GetWebActions(string fileId)
        {
            using (var driveService = CreateDriveService())
            {
                var actions = new List<WebAction>();
            
                var request = driveService.Files.Get(fileId);
                
                request.Fields = "id, name, mimeType, webViewLink, webContentLink";

                var data = await request.ExecuteAsync();

                if (data != null)
                {
                    var webViewAction = new WebAction("View on Web", data.WebViewLink);
                    var downloadAction = new WebAction("Download", data.WebContentLink);

                    actions.Add(webViewAction);
                    actions.Add(downloadAction);
                }
                
                return actions;
            }
        }

        public async Task<File> CreateFileFromId(string fileId)
        {
            using (var driveService = CreateDriveService())
            {
                var request = driveService.Files.Get(fileId);

                request.Fields = "id, name, mimeType";

                var data = await request.ExecuteAsync();

                var file = new File
                {
                    Id = Guid.NewGuid(),
                    FileId = data.Id,
                    FileName = data.Name,
                    ContentType = data.MimeType
                };

                return file;
            }
        }
    }
}
