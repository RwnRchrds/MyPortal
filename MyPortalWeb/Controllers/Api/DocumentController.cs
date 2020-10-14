using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/document")]
    public class DocumentController : BaseApiController
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IUserService userService, IAcademicYearService academicYearService, IDocumentService documentService) : base(userService, academicYearService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetById([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                var document = await _documentService.GetDocumentById(documentId);

                return Ok(document);
            });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] CreateDocumentModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await UserService.GetUserByPrincipal(User);

                var document = new DocumentModel
                {
                    Title = model.Title,
                    Description = model.Description,
                    DirectoryId = model.DirectoryId,
                    CreatedById = user.Id,
                    TypeId = model.TypeId,
                    Restricted = model.Restricted
                };

                await _documentService.Create(document);

                return Ok("Document created successfully.");
            });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromForm] UpdateDocumentModel model)
        {
            return await ProcessAsync(async () =>
            {
                var document = new DocumentModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    TypeId = model.TypeId,
                    Restricted = model.Restricted
                };

                await _documentService.Update(document);

                return Ok("Document updated successfully.");
            });
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                await _documentService.Delete(documentId);

                return Ok("Document deleted successfully.");
            });
        }

        [HttpGet]
        [Route("Download")]
        public async Task<IActionResult> DownloadFile([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                var download = await _documentService.GetDownloadByDocument(documentId);

                return File(download.FileStream, download.ContentType, download.FileName);
            });
        }

        [HttpGet]
        [Route("View")]
        public async Task<IActionResult> GetWebViewLink([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                var file = await _documentService.GetFileMetadataByDocument(documentId);

                return Ok(file.WebViewLink);
            });
        }

        public override void Dispose()
        {
            _documentService.Dispose();
        }
    }
}
