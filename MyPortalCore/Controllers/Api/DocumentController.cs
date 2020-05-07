using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Google;

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

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> DownloadFile([FromQuery] Guid documentId, [FromQuery] bool downloadAsPdf = false)
        {
            return await Process(async () =>
            {
                var download = await _documentService.GetDownloadById(documentId, downloadAsPdf);

                return File(download.FileStream, download.ContentType, download.FileName);
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddExistingFile([FromForm] )
    }
}
