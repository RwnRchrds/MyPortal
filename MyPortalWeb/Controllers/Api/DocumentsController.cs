using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/documents")]
    public class DocumentsController : BaseApiController
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IUserService userService, IAcademicYearService academicYearService,
            IDocumentService documentService, IRolePermissionsCache rolePermissionsCache) : base(userService,
            academicYearService, rolePermissionsCache)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Route("get")]
        [Produces(typeof(DocumentModel))]
        public async Task<IActionResult> GetById([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                var document = await _documentService.GetDocumentById(documentId);

                return Ok(document);
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateDocumentModel model)
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
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateDocumentModel model)
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
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                await _documentService.Delete(documentId);

                return Ok("Document deleted successfully.");
            });
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> DownloadFile([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                var download = await _documentService.GetDownloadByDocument(documentId);

                return File(download.FileStream, download.ContentType, download.FileName);
            });
        }

        [HttpGet]
        [Route("view")]
        [Produces(typeof(string))]
        public async Task<IActionResult> GetWebViewLink([FromQuery] Guid documentId)
        {
            return await ProcessAsync(async () =>
            {
                var file = await _documentService.GetFileMetadataByDocument(documentId);

                if (file is HostedFileMetadata hostedMetadata)
                {
                    return Ok(hostedMetadata.WebViewLink);
                }

                return BadRequest("You are not using a 3rd party file provider.");
            });
        }

        public override void Dispose()
        {
            _documentService.Dispose();
        }
    }
}
