using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.DocumentProvision;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class HostedFileService : BaseService, IFileService
    {
        private readonly IHostedFileProvider _fileProvider;

        public HostedFileService(IHostedFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task AttachFileToDocument(Guid documentId, string fileId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
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

                unitOfWork.Files.Create(file);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task RemoveFileFromDocument(Guid documentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var file = await unitOfWork.Files.GetByDocumentId(documentId);

                if (file == null)
                {
                    throw new LogicException("No file is attached to this document.");
                }

                await unitOfWork.Files.Delete(file.Id);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<HostedFileMetadata> GetMetadataByDocument(Guid documentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var file = await unitOfWork.Files.GetByDocumentId(documentId);

                var metadata = await _fileProvider.FetchMetadata(file.FileId);

                if (metadata == null)
                {
                    throw new NotFoundException("File not found.");
                }

                return metadata;
            }
        }

        public async Task<FileDownload> GetDownloadByDocument(Guid documentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var file = await unitOfWork.Files.GetByDocumentId(documentId);

                var stream = await _fileProvider.DownloadFileToStream(file.FileId);

                return new FileDownload(stream, file.ContentType, file.FileName);
            }
        }
    }
}
