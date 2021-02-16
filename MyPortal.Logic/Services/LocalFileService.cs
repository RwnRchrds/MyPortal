using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.Services
{
    public class LocalFileService : IFileService
    {
        private readonly ILocalFileProvider _fileProvider;
        private readonly IFileRepository _fileRepository;

        public LocalFileService(ILocalFileProvider fileProvider, IFileRepository fileRepository)
        {
            _fileProvider = fileProvider;
            _fileRepository = fileRepository;
        }

        public async Task UploadFileToDocument(UploadAttachmentModel upload)
        {
            var existingFile = await _fileRepository.GetByDocumentId(upload.DocumentId);

            if (existingFile != null)
            {
                throw new LogicException("A file is already attached to this document.");
            }

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

        public async Task RemoveFileFromDocument(Guid documentId)
        {
            var file = await _fileRepository.GetByDocumentId(documentId);

            if (file == null)
            {
                throw new LogicException("No file is attached to this document.");
            }

            _fileProvider.DeleteFile(file.FileId);

            await _fileRepository.Delete(file.Id);

            await _fileRepository.SaveChanges();
        }
    }
}
