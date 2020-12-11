﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Filters;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.FileProviders;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IDirectoryService _directoryService;
        private readonly IFileProvider _fileProvider;
        private readonly IFileRepository _fileRepository;

        public DocumentService(IDocumentRepository documentRepository, IDocumentTypeRepository documentTypeRepository,
            IDirectoryService directoryService, IFileProvider fileProvider, IFileRepository fileRepository)
        {
            _documentRepository = documentRepository;
            _documentTypeRepository = documentTypeRepository;
            _directoryService = directoryService;
            _fileProvider = fileProvider;
            _fileRepository = fileRepository;
        }

        public async Task Create(params DocumentModel[] documents)
        {
            foreach (var document in documents)
            {
                var directory = await _directoryService.GetById(document.DirectoryId);

                if (directory == null)
                {
                    throw new NotFoundException("Directory not found.");
                }

                try
                {
                    var docToAdd = new Document
                    {
                        TypeId = document.TypeId,
                        Title = document.Title,
                        Description = document.Description,
                        CreatedDate = DateTime.Today,
                        DirectoryId = document.DirectoryId,
                        CreatedById = document.CreatedById,
                        Deleted = false,
                        Restricted = document.Restricted
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
                documentInDb.Restricted = document.Restricted;
                documentInDb.TypeId = document.TypeId;
            }

            await _documentRepository.SaveChanges();
        }

        public async Task<IEnumerable<DocumentTypeModel>> GetTypes(DocumentTypeFilter filter)
        {
            var documentTypes = await _documentTypeRepository.Get(filter);

            return documentTypes.Select(BusinessMapper.Map<DocumentTypeModel>).ToList();
        }

        public async Task Delete(params Guid[] documentIds)
        {
            foreach (var documentId in documentIds)
            {
                await _documentRepository.Delete(documentId);
            }

            await _documentRepository.SaveChanges();
        }

        public async Task<FileMetadata> GetFileMetadataByDocument(Guid documentId)
        {
            var file = await _fileRepository.GetByDocumentId(documentId);

            if (_fileProvider is HostedFileProvider hostingService)
            {
                return await hostingService.FetchMetadata(file.FileId);
            }
            
            return new FileMetadata
            {
                Id = file.FileId,
                Name = file.FileName,
                MimeType = file.ContentType
            };
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

        public async Task UploadAttachmentToDocument(UploadAttachmentModel upload)
        {
            var existingFile = await _fileRepository.GetByDocumentId(upload.DocumentId);

            if (existingFile != null)
            {
                throw new LogicException("A file is already attached to this document.");
            }

            var fileExtension = Path.GetExtension(upload.File.FileName);

            string fileId = await _fileProvider.UploadFile(upload);

            var file = new Database.Models.Entity.File
            {
                FileId = fileId,
                FileName = upload.File.FileName,
                ContentType = upload.File.ContentType,
                DocumentId = upload.DocumentId
            };

            _fileRepository.Create(file);

            await _fileRepository.SaveChanges();
        }

        public async Task<FileDownload> GetDownloadByDocument(Guid documentId)
        {
            var file = await _fileRepository.GetByDocumentId(documentId);

            var stream = await _fileProvider.DownloadFileToStream(file.FileId);

            return new FileDownload(stream, file.ContentType, file.FileName);
        }

        public override void Dispose()
        {
            _documentRepository.Dispose();
            _documentTypeRepository.Dispose();
            _directoryService.Dispose();
            _fileRepository.Dispose();
            _fileProvider.Dispose();
        }
    }
}
