using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using MyPortal.Logic.Models.Web;
using MyPortal.Logic.Services;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/documents")]
    public class DocumentsController : BaseApiController
    {
        private IDocumentService _documentService;
        private IFileService _fileService;

        public DocumentsController(IUserService userService, IRoleService roleService, IDocumentService documentService,
            IFileService fileService)
            : base(userService, roleService)
        {
            _documentService = documentService;
            _fileService = fileService;
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(DocumentModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid documentId)
        {
            try
            {
                var document = await _documentService.GetDocumentById(documentId);

                return Ok(document);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateDocumentModel model)
        {
            try
            {
                await _documentService.Create(model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("file/upload")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadFile([FromBody] UploadAttachmentModel model)
        {
            try
            {
                if (_fileService is LocalFileService localFileService)
                {
                    await localFileService.UploadFileToDocument(model);

                    return Ok();
                }

                return BadRequest(
                    "MyPortal is currently configured to use a 3rd party file provider. Please use LinkHostedFile endpoint instead.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("file/linkHosted")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LinkHostedFile([FromBody] LinkHostedFileModel model)
        {
            try
            {
                if (_fileService is HostedFileService hostedFileService)
                {
                    await hostedFileService.AttachFileToDocument(model.DocumentId, model.FileId);

                    return Ok();
                }

                return BadRequest(
                    "MyPortal is currently configured to host files locally. Please use UploadFile endpoint instead.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("file/{documentId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveAttachment([FromRoute] Guid documentId)
        {
            try
            {
                await _fileService.RemoveFileFromDocument(documentId);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateDocumentModel model)
        {
            try
            {
                await _documentService.Update(model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] Guid documentId)
        {
            try
            {
                await _documentService.Delete(documentId);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("file/{documentId}")]
        [ProducesResponseType(typeof(Stream), 200)]
        public async Task<IActionResult> DownloadFile([FromRoute] Guid documentId)
        {
            try
            {
                var download = await _fileService.GetDownloadByDocument(documentId);

                return File(download.FileStream, download.ContentType, download.FileName);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("webActions")]
        [ProducesResponseType(typeof(IEnumerable<WebAction>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWebActions([FromQuery] Guid documentId)
        {
            try
            {
                if (_fileService is HostedFileService hostedFileService)
                {
                    var webActions = await hostedFileService.GetWebActionsByDocument(documentId);

                    return Ok(webActions);
                }

                return Ok(new List<WebAction>());
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
