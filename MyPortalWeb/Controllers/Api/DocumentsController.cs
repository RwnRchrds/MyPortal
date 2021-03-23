using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using MyPortal.Logic.Services;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/documents")]
    public class DocumentsController : BaseApiController
    {
        public DocumentsController(IAppServiceCollection services) : base(services)
        {
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(DocumentModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                var document = await Services.Documents.GetDocumentById(documentId);

                return Ok(document);
            });
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateDocumentModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await Services.Users.GetUserByPrincipal(User);

                var document = new DocumentModel
                {
                    Title = model.Title,
                    Description = model.Description,
                    DirectoryId = model.DirectoryId,
                    CreatedById = user.Id,
                    TypeId = model.TypeId,
                    Restricted = model.Restricted
                };

                await Services.Documents.Create(document);

                return Ok();
            });
        }

        [HttpPost]
        [Route("file/upload")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadFile([FromBody] UploadAttachmentModel model)
        {
            return await ProcessAsync(async () =>
            {
                if (Services.Files is LocalFileService localFileService)
                {
                    await localFileService.UploadFileToDocument(model);

                    return Ok();
                }

                return BadRequest(
                    "MyPortal is currently configured to use a 3rd party file provider. Please use LinkHostedFile instead.");
            });
        }

        [HttpPost]
        [Route("file/linkHosted")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LinkHostedFile([FromBody] LinkHostedFileModel model)
        {
            return await ProcessAsync(async () =>
            {
                if (Services.Files is HostedFileService hostedFileService)
                {
                    await hostedFileService.AttachFileToDocument(model.DocumentId, model.FileId);

                    return Ok();
                }

                return BadRequest(
                    "MyPortal is currently configured to host files locally. Please use UploadFile instead.");
            });
        }

        [HttpDelete]
        [Route("file/{documentId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveAttachment([FromRoute] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                await Services.Files.RemoveFileFromDocument(documentId);

                return Ok();
            });
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateDocumentModel model)
        {
            return await ProcessAsync(async () =>
            {
                await Services.Documents.Update(model);

                return Ok();
            });
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                await Services.Documents.Delete(documentId);

                return Ok();
            });
        }

        [HttpGet]
        [Route("file/{documentId}")]
        [ProducesResponseType(typeof(Stream), 200)]
        public async Task<IActionResult> DownloadFile([FromRoute] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                var download = await Services.Files.GetDownloadByDocument(documentId);

                return File(download.FileStream, download.ContentType, download.FileName);
            });
        }

        [HttpGet]
        [Route("view")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWebViewLink([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                if (Services.Files is HostedFileService hostedFileService)
                {
                    var file = await hostedFileService.GetMetadataByDocument(documentId);

                    return Ok(file.WebViewLink);
                }

                return BadRequest("You are not using a 3rd party file provider.");
            });
        }
    }
}
