using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.Services
{
    public class LocalFileService : BaseService, IFileService
    {
        private readonly ILocalFileProvider _fileProvider;

        public LocalFileService(ILocalFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task UploadFileToDocument(UploadAttachmentModel upload)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var document = await unitOfWork.Documents.GetById(upload.DocumentId);

                if (document == null)
                {
                    throw new NotFoundException("Document not found.");
                }

                if (document.FileId.HasValue)
                {
                    throw new LogicException("A file is already attached to this document.");
                }

                string fileId = await _fileProvider.SaveFile(upload);

                document.Attachment = new Database.Models.Entity.File
                {
                    FileId = fileId,
                    FileName = upload.File.FileName,
                    ContentType = upload.File.ContentType
                };

                await unitOfWork.Documents.Update(document);
                
                await unitOfWork.SaveChangesAsync();
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

        public async Task RemoveFileFromDocument(Guid documentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var file = await unitOfWork.Files.GetByDocumentId(documentId);

                if (file == null)
                {
                    throw new LogicException("No file is attached to this document.");
                }

                _fileProvider.DeleteFile(file.FileId);

                await unitOfWork.Files.Delete(file.Id);

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
