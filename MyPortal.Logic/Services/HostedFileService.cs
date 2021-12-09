using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Web;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class HostedFileService : BaseService, IFileService
    {
        private readonly IHostedFileProvider _fileProvider;

        public HostedFileService(ClaimsPrincipal user, IHostedFileProvider fileProvider) : base(user) 
        {
            _fileProvider = fileProvider;
        }

        public async Task AttachFileToDocument(Guid documentId, string fileId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var document = await unitOfWork.Documents.GetById(documentId);

                if (document == null)
                {
                    throw new NotFoundException("Document not found.");
                }

                var file = await _fileProvider.GetFileById(fileId);

                document.Attachment = file;
                
                await unitOfWork.Documents.Update(document);

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

        public async Task<IEnumerable<WebAction>> GetWebActionsByDocument(Guid documentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var file = await unitOfWork.Files.GetByDocumentId(documentId);

                var webActions = await _fileProvider.GetWebActions(file.FileId);

                return webActions;
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
