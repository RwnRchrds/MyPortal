using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
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
        [Route("getById", Name = "ApiDocumentGetById")]
        public async Task<IActionResult> GetById([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                var document = await _documentService.GetDocumentById(documentId);

                return Ok(document);
            });
        }

        [HttpPost]
        [Route("create", Name = "ApiDocumentCreate")]
        public async Task<IActionResult> Create([FromForm] CreateDocumentModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await _userService.GetUserByPrincipal(User);

                var document = new DocumentModel
                {
                    Title = model.Title,
                    Description = model.Description,
                    DirectoryId = model.DirectoryId,
                    Public = model.Public,
                    CreatedById = user.Id,
                    FileId = model.FileId,
                    TypeId = model.TypeId
                };

                await _documentService.Create(document);

                return Ok("Document created successfully.");
            });
        }

        [HttpPut]
        [Route("update", Name = "ApiDocumentUpdate")]
        public async Task<IActionResult> Update([FromForm] UpdateDocumentModel model)
        {
            return await ProcessAsync(async () =>
            {
                var document = new DocumentModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    Approved = model.Approved,
                    TypeId = model.TypeId,
                    Public = model.Public
                };

                await _documentService.Update(document);

                return Ok("Document updated successfully.");
            });
        }

        [HttpDelete]
        [Route("delete", Name = "ApiDocumentDelete")]
        public async Task<IActionResult> Delete([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                await _documentService.Delete(documentId);

                return Ok("Document deleted successfully.");
            });
        }

        [HttpGet]
        [Route("download", Name = "ApiDocumentDownload")]
        public async Task<IActionResult> DownloadFile([FromQuery] Guid documentId, [FromQuery] bool asPdf = false)
        {
            return await ProcessAsync(async () =>
            {
                var download = await _documentService.GetDownloadById(documentId, asPdf);

                return File(download.FileStream, download.ContentType, download.FileName);
            });
        }

        [HttpGet]
        [Route("getLink", Name = "ApiDocumentGetWebViewLink")]
        public async Task<IActionResult> GetWebViewLink([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                var file = await _documentService.GetFileById(documentId);

                return Ok(file.WebViewLink);
            });
        }

        public override void Dispose()
        {
            _documentService.Dispose();
        }
    }
}
