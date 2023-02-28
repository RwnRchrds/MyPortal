using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
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
        private readonly string _accessToken;

        public HostedFileService(IHostedFileProvider fileProvider, string accessToken)
        {
            _fileProvider = fileProvider;
            _accessToken = accessToken;
        }

        public async Task AttachFileToDocument(Guid documentId, string fileId)
        {
            await using var unitOfWork = await DataConnectionFactory.CreateUnitOfWork();
            
            var document = await unitOfWork.Documents.GetById(documentId);

            if (document == null)
            {
                throw new NotFoundException("Document not found.");
            }

            var file = await _fileProvider.CreateFileFromId(_accessToken, fileId);

            document.Attachment = file;
                
            await unitOfWork.Documents.Update(document);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveFileFromDocument(Guid documentId)
        {
            await using var unitOfWork = await DataConnectionFactory.CreateUnitOfWork();
            
            var file = await unitOfWork.Files.GetByDocumentId(documentId);

            if (file == null)
            {
                throw new LogicException("No file is attached to this document.");
            }

            await unitOfWork.Files.Delete(file.Id);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<WebAction>> GetWebActionsByDocument(Guid documentId)
        {
            await using var unitOfWork = await DataConnectionFactory.CreateUnitOfWork();
            
            var file = await unitOfWork.Files.GetByDocumentId(documentId);

            var webActions = await _fileProvider.GetWebActions(_accessToken, file.FileId);

            return webActions;
        }
    }
}
