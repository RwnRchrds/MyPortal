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
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Filters;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Exceptions;
using MyPortal.Logic.Models.Google;
using MyPortal.Logic.Models.Requests.Documents;
using File = Google.Apis.Drive.v3.Data.File;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        private IGoogleHelper _googleHelper;
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IDirectoryService _directoryService;
        private DriveService _driveService;

        public DocumentService(IDocumentRepository documentRepository, IGoogleHelper googleHelper, IDirectoryService directoryService, IDocumentTypeRepository documentTypeRepository) : base("Document")
        {
            _documentRepository = documentRepository;
            _googleHelper = googleHelper;
            _directoryService = directoryService;
            _documentTypeRepository = documentTypeRepository;

            var init = _googleHelper.GetInitializer();

            _driveService = new DriveService(init);
        }

        //public async Task Create(params DocumentUpload[] uploads)
        //{
        //    foreach (var upload in uploads)
        //    {
        //        var docToAdd = new Document
        //        {
        //            TypeId = upload.Details.TypeId,
        //            Title = upload.Details.Title,
        //            Description = upload.Details.Description,
        //            UploadedDate = DateTime.Now,
        //            DirectoryId = upload.Details.DirectoryId,
        //            UploaderId = upload.Details.UploaderId,
        //            Public = upload.Details.Public,
        //            Deleted = false,
        //            Approved = false
        //        };

        //        var metadata = new File
        //        {
        //            Name = upload.Details.FileName,
        //            Description = upload.Details.Description
        //        };

        //        FilesResource.CreateMediaUpload request;

        //        string mimeType;

        //        using (upload.FileStream)
        //        {
        //            mimeType = MimeTypeHelper.GetMimeType(Path.GetExtension(upload.FileStream.Name));

        //            request = _driveService.Files.Create(metadata, upload.FileStream, mimeType);
        //            request.Fields = "id, mimeType";

        //            await request.UploadAsync();
        //        }

        //        var file = request.ResponseBody;

        //        docToAdd.FileId = file.Id;
        //        docToAdd.ContentType = mimeType;

        //        _documentRepository.Create(docToAdd);
        //    }

        //    await _documentRepository.SaveChanges();
        //}

        public async Task Create(params DocumentModel[] documents)
        {
            foreach (var document in documents)
            {
                var request = _driveService.Files.Get(document.FileId);

                request.Fields = "id, name, description, mimeType";

                request.SupportsAllDrives = true;

                var directory = await _directoryService.GetById(document.DirectoryId);

                if (directory == null)
                {
                    throw NotFound("Directory not found.");
                }

                try
                { 
                    var documentInCloud = await request.ExecuteAsync();

                    var docToAdd = new Document
                    {
                        TypeId = document.TypeId,
                        Title = document.Title,
                        Description = document.Description,
                        UploadedDate = DateTime.Today,
                        DirectoryId = document.DirectoryId,
                        UploaderId = document.UploaderId,
                        Public = !directory.Private || document.Public,
                        Deleted = false,
                        Approved = directory.Private || document.Approved,
                        ContentType = documentInCloud.MimeType,
                        FileName = documentInCloud.Name,
                        FileId = document.FileId
                    };

                    _documentRepository.Create(docToAdd);
                }
                catch (Exception e)
                {
                    throw BadRequest(e);
                }
            }

            await _documentRepository.SaveChanges();
        }

        public async Task Update(params DocumentModel[] documents)
        {
            foreach (var document in documents)
            {
                var documentInDb = await _documentRepository.GetByIdWithTracking(document.Id);

                documentInDb.Title = document.Title;
                documentInDb.Description = document.Description;
                documentInDb.Approved = document.Approved;
                documentInDb.TypeId = document.TypeId;
            }

            await _documentRepository.SaveChanges();
        }

        public async Task<Lookup> GetTypes(Guid searchFilter)
        {
            var filter = new DocumentTypeFilter();

            if (searchFilter == SearchFilters.DocumentTypes.Student)
            {
                filter.Active = true;   
                filter.Student = true;
            }
            else if(searchFilter == SearchFilters.DocumentTypes.Staff)
            {
                filter.Active = true;
                filter.Staff = true;
            }
            else if (searchFilter == SearchFilters.DocumentTypes.Contact)
            {
                filter.Active = true;
                filter.Staff = true;
            }
            else if (searchFilter == SearchFilters.DocumentTypes.General)
            {
                filter.Active = true;
                filter.General = true;
            }
            else if (searchFilter == SearchFilters.DocumentTypes.Sen)
            {
                filter.Active = true;
                filter.Sen = true;
            }
            else if (searchFilter == SearchFilters.DocumentTypes.Active)
            {
                filter.Active = true;
            }

            var documentTypes = await _documentTypeRepository.Get(filter);

            return new Lookup(documentTypes.ToDictionary(x => x.Description, x => x.Id));
        }

        public async Task Delete(params Guid[] documentIds)
        {
            foreach (var documentId in documentIds)
            {
                await _documentRepository.Delete(documentId);
            }

            await _documentRepository.SaveChanges();
        }

        public async Task<File> GetFileById(Guid documentId)
        {
            var document = await _documentRepository.GetById(documentId);

            if (document == null)
            {
                throw NotFound();
            }

            try
            {
                var request = _driveService.Files.Get(document.FileId);

                request.SupportsAllDrives = true;

                request.Fields = "id, name, webViewLink, mimeType, size, version, fileExtension, description, permissions";

                return await request.ExecuteAsync(CancellationToken.None);
            }
            catch (Exception e)
            {
                throw BadRequest(e);
            }
        }

        public async Task<DocumentModel> GetDocumentById(Guid documentId)
        {
            var document = await _documentRepository.GetById(documentId);

            if (document == null)
            {
                throw NotFound();
            }

            return _businessMapper.Map<DocumentModel>(document);
        }

        public async Task<FileDownload> GetDownloadById(Guid documentId, bool downloadAsPdf = false)
        {
            var document = await _documentRepository.GetById(documentId);

            if (document == null)
            {
                throw NotFound();
            }

            var googleMimeTypes = GoogleMimeTypes.GetAll();

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

        public override void Dispose()
        {
            _documentRepository.Dispose();
            _driveService.Dispose();
        }
    }
}
