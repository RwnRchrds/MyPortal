using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Google;
using MyPortal.Logic.Models.Requests.Documents;
using File = Google.Apis.Drive.v3.Data.File;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        private IGoogleService _googleService;
        private readonly IDocumentRepository _repository;
        private DriveService _driveService;

        public DocumentService(IDocumentRepository repository, IGoogleService googleService)
        {
            _repository = repository;
            _googleService = googleService;

            var init = _googleService.GetInitializer();

            _driveService = new DriveService(init);
        }

        public async Task Create(params DocumentUpload[] uploads)
        {
            foreach (var upload in uploads)
            {
                var docToAdd = new Document
                {
                    TypeId = upload.Details.TypeId,
                    Title = upload.Details.Title,
                    Description = upload.Details.Description,
                    UploadedDate = DateTime.Now,
                    DirectoryId = upload.Details.DirectoryId,
                    UploaderId = upload.Details.UploaderId,
                    Public = upload.Details.Public,
                    Deleted = false,
                    Approved = false
                };

                var metadata = new File
                {
                    Name = upload.Details.FileName,
                    Description = upload.Details.Description
                };

                FilesResource.CreateMediaUpload request;

                string mimeType;

                using (upload.FileStream)
                {
                    mimeType = MimeTypeHelper.GetMimeType(Path.GetExtension(upload.FileStream.Name));

                    request = _driveService.Files.Create(metadata, upload.FileStream, mimeType);
                    request.Fields = "id, mimeType";

                    await request.UploadAsync();
                }

                var file = request.ResponseBody;

                docToAdd.FileId = file.Id;
                docToAdd.ContentType = mimeType;

                _repository.Create(docToAdd);
            }

            await _repository.SaveChanges();
        }

        public async Task Create(params DocumentModel[] documents)
        {
            foreach (var document in documents)
            {
                var request = _driveService.Files.Get(document.FileId);

                request.Fields = "id, name, description, mimeType";

                var documentInCloud = await request.ExecuteAsync();

                var docToAdd = new Document
                {
                    TypeId = document.TypeId,
                    Title = document.Title,
                    Description = document.Description,
                    UploadedDate = DateTime.Now,
                    DirectoryId = document.DirectoryId,
                    UploaderId = document.UploaderId,
                    Public = document.Public,
                    Deleted = false,
                    Approved = false,
                    ContentType = documentInCloud.MimeType,
                    FileName = documentInCloud.Name,
                    FileId = document.FileId
                };

                _repository.Create(docToAdd);
            }

            await _repository.SaveChanges();
        }

        public async Task<File> GetFileById(Guid documentId)
        {
            var document = await _repository.GetById(documentId);

            try
            {
                var request = _driveService.Files.Get(document.FileId);

                request.Fields = "id, name, webViewLink, mimeType, size, version, fileExtension, description, permissions";

                return await request.ExecuteAsync(CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<DocumentModel> GetDocumentById(Guid documentId)
        {
            var document = await _repository.GetById(documentId);

            return _businessMapper.Map<DocumentModel>(document);
        }

        public async Task<FileDownload> GetDownloadById(Guid documentId, bool downloadAsPdf = false)
        {
            var googleMimeTypes = GoogleMimeTypes.GetAll();

            var document = await _repository.GetById(documentId);

            var request = _driveService.Files.Get(document.FileId);

            request.SupportsAllDrives = true;

            request.Fields = "id, name, mimeType, fileExtension";

            var fileInfo = await request.ExecuteAsync(CancellationToken.None);

            var mimeType = fileInfo.MimeType;

            var fileStream = new MemoryStream();

            if (downloadAsPdf)
            {
                mimeType = MimeTypeHelper.GetMimeType(".pdf");

                var pdf = _driveService.Files.Export(document.FileId, mimeType);

                await pdf.DownloadAsync(fileStream);
            }
            else if (googleMimeTypes.Contains(fileInfo.MimeType))
            {
                mimeType = GoogleMimeTypes.GetExportMimeType(fileInfo.MimeType);

                var export = _driveService.Files.Export(document.FileId, mimeType);

                await export.DownloadAsync(fileStream);
            }
            else
            {
                await request.DownloadAsync(fileStream);
            }

            fileStream.Position = 0;

            var fileName = fileInfo.Name;

            var fileExtension = MimeTypeHelper.GetExtension(mimeType);

            if (!fileName.EndsWith(fileExtension))
            {
                fileName = $"{fileName}{fileExtension}";
            }

            return new FileDownload(fileStream, fileInfo.MimeType, fileName);
        }
    }
}
