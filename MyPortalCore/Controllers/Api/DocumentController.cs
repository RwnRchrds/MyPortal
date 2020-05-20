using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Google;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortalCore.Controllers.Api
{
    [Route("api/document")]
    [ApiController]
    public class DocumentController : BaseApiController
    {
        private IDocumentService _documentService;

        public DocumentController(IApplicationUserService userService, IDocumentService documentService) : base(userService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Route("getMetadata")]
        public async Task<IActionResult> GetMetadata([FromQuery] Guid documentId)
        {
            return await Process(async () =>
            {
                var document = await _documentService.GetDocumentById(documentId);

                return Ok(document);
            });
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadExisting([FromForm] CreateDocumentModel model)
        {
            return await Process(async () =>
            {
                var user = await _userService.GetUserByPrincipal(User);

                var document = new DocumentModel
                {
                    Title = model.Title,
                    Description = model.Description,
                    DirectoryId = model.DirectoryId,
                    Public = model.Public,
                    UploaderId = user.Id,
                    FileId = model.FileId,
                    TypeId = model.TypeId
                };

                await _documentService.Create(document);

                return Ok("Document uploaded.");
            });
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> DownloadFile([FromQuery] Guid documentId, [FromQuery] bool asPdf = false)
        {
            return await Process(async () =>
            {
                var download = await _documentService.GetDownloadById(documentId, asPdf);

                return File(download.FileStream, download.ContentType, download.FileName);
            });
        }

        public override void Dispose()
        {
            _documentService.Dispose();
        }
    }
}
