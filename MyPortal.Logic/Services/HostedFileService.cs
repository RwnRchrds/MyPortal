using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.DocumentProvision;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class HostedFileService : IFileService
    {
        private readonly IHostedFileProvider _fileProvider;
        private readonly IFileRepository _fileRepository;

        public HostedFileService(IHostedFileProvider fileProvider, IFileRepository fileRepository)
        {
            _fileProvider = fileProvider;
            _fileRepository = fileRepository;
        }

        public async Task AttachFileToDocument(Guid documentId, string fileId)
        {
            var fileData = await _fileProvider.FetchMetadata(fileId);

            if (fileData == null)
            {
                throw new NotFoundException("The selected file was not found.");
            }

            var file = new File
            {
                ContentType = fileData.MimeType,
                DocumentId = documentId,
                FileId = fileData.Id,
                FileName = fileData.Name
            };

            _fileRepository.Create(file);

            await _fileRepository.SaveChanges();
        }

        public async Task RemoveFileFromDocument(Guid documentId)
        {
            var file = await _fileRepository.GetByDocumentId(documentId);

            if (file == null)
            {
                throw new LogicException("No file is attached to this document.");
            }

            await _fileRepository.Delete(file.Id);

            await _fileRepository.SaveChanges();
        }

        public async Task<HostedFileMetadata> GetMetadataByDocument(Guid documentId)
        {
            var file = await _fileRepository.GetByDocumentId(documentId);

            var metadata = await _fileProvider.FetchMetadata(file.FileId);

            if (metadata == null)
            {
                throw new NotFoundException("File not found.");
            }

            return metadata;
        }

        public async Task<FileDownload> GetDownloadByDocument(Guid documentId)
        {
            var file = await _fileRepository.GetByDocumentId(documentId);

            var stream = await _fileProvider.DownloadFileToStream(file.FileId);

            return new FileDownload(stream, file.ContentType, file.FileName);
        }
    }
}
