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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Filters;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Exceptions;
using MyPortal.Logic.Models.Requests.Documents;
using File = Google.Apis.Drive.v3.Data.File;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IDirectoryService _directoryService;
        private readonly DriveService _driveService;

        public DocumentService(IConfiguration config, ApplicationDbContext context)
        {
            var connection = context.Database.GetDbConnection();
            _documentRepository = new DocumentRepository(context);
            _directoryService = new DirectoryService(context);
            _documentTypeRepository = new DocumentTypeRepository(connection);

            var googleHelper = new GoogleHelper(config);
            _driveService = new DriveService(googleHelper.GetInitializer());
        }

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
                    throw new NotFoundException("Directory not found.");
                }

                try
                { 
                    var documentInCloud = await request.ExecuteAsync();

                    var docToAdd = new Document
                    {
                        TypeId = document.TypeId,
                        Title = document.Title,
                        Description = document.Description,
                        CreatedDate = DateTime.Today,
                        DirectoryId = document.DirectoryId,
                        CreatedById = document.CreatedById,
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
                    throw GetInnerException(e);
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

        public async Task<Lookup> GetTypes(DocumentTypeFilter filter)
        {
            var documentTypes = await _documentTypeRepository.Get(filter);

            return documentTypes.ToLookup();
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
                throw new NotFoundException("Document not found.");
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
                throw GetInnerException(e);
            }
        }

        public async Task<DocumentModel> GetDocumentById(Guid documentId)
        {
            var document = await _documentRepository.GetById(documentId);

            if (document == null)
            {
                throw new NotFoundException("Document not found.");
            }

            return BusinessMapper.Map<DocumentModel>(document);
        }

        public async Task<FileDownload> GetDownloadById(Guid documentId, bool downloadAsPdf = false)
        {
            var document = await _documentRepository.GetById(documentId);

            if (document == null)
            {
                throw new NotFoundException("Document not found.");
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
            _documentTypeRepository.Dispose();
            _directoryService.Dispose();
            _driveService.Dispose();
        }
    }
}
