using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.Services
{
    public class LocalFileService : BaseService, IFileService
    {
        private readonly ILocalFileProvider _fileProvider;

        public LocalFileService(IUnitOfWork unitOfWork, ILocalFileProvider fileProvider) : base(unitOfWork)
        {
            _fileProvider = fileProvider;
        }

        public async Task UploadFileToDocument(UploadAttachmentModel upload)
        {
            var existingFile = await UnitOfWork.Files.GetByDocumentId(upload.DocumentId);

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

            UnitOfWork.Files.Create(file);

            await UnitOfWork.SaveChanges();
        }

        public async Task<FileDownload> GetDownloadByDocument(Guid documentId)
        {
            var file = await UnitOfWork.Files.GetByDocumentId(documentId);

            var stream = await _fileProvider.DownloadFileToStream(file.FileId);

            return new FileDownload(stream, file.ContentType, file.FileName);
        }

        public async Task RemoveFileFromDocument(Guid documentId)
        {
            var file = await UnitOfWork.Files.GetByDocumentId(documentId);

            if (file == null)
            {
                throw new LogicException("No file is attached to this document.");
            }

            _fileProvider.DeleteFile(file.FileId);

            await UnitOfWork.Files.Delete(file.Id);

            await UnitOfWork.SaveChanges();
        }
    }
}
