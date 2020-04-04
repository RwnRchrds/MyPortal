using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Documents;
using File = Google.Apis.Drive.v3.Data.File;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DocumentService : GSuiteIntegrationService, IDocumentService
    {
        private readonly IDocumentRepository _repository;
        private DriveService _driveService;

        public DocumentService(IDocumentRepository repository)
        {
            _repository = repository;
        }



        public async Task Create(Guid userId, params DocumentUpload[] documents)
        {
            if (_driveService == null)
            {
                var init = await GetInitializer(userId);

                _driveService = new DriveService(init);
            }

            foreach (var document in documents)
            {
                var docToAdd = new Document
                {
                    TypeId = document.Details.TypeId,
                    Title = document.Details.Title,
                    Description = document.Details.Description,
                    FileName = document.Details.FileName,
                    UploadedDate = DateTime.Now,
                    DirectoryId = document.Details.DirectoryId
                };

                var metadata = new File
                {
                    Name = document.Details.FileName,
                };

                FilesResource.CreateMediaUpload request;

                using (document.File)
                {
                    request = _driveService.Files.Create(metadata, document.File,
                        MimeTypeHelper.GetMimeType(Path.GetExtension(document.File.Name)));
                    request.Fields = "id";

                    await request.UploadAsync();
                }

                var file = request.ResponseBody;

                docToAdd.FileId = file.Id;

                _repository.Create(docToAdd);
            }

            await _repository.SaveChanges();
        }

        public async Task<string> GetUrl(Guid userId, Guid documentId)
        {
            if (_driveService == null)
            {
                var init = await GetInitializer(userId);

                _driveService = new DriveService(init);
            }

            var document = await _repository.GetById(documentId);

            var file = await _driveService.Files.Get(document.FileId).ExecuteAsync();

            return file.WebViewLink;
        }

        public async Task<Stream> Download(Guid userId, Guid documentId)
        {
            var document = await _repository.GetById(documentId);

            using (var stream = new MemoryStream())
            {
                var request = _driveService.Files.Get(document.FileId);

                await request.DownloadAsync(stream);

                return stream;
            }
        }
    }
}
