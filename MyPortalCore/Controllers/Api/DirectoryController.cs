using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;

namespace MyPortalCore.Controllers.Api
{
    [Route("api/directory")]
    [ApiController]
    public class DirectoryController : BaseApiController
    {
        private readonly IDirectoryService _directoryService;
        private readonly IDocumentService _documentService;
        public DirectoryController(IApplicationUserService userService, IDirectoryService directoryService, IDocumentService documentService) : base(userService)
        {
            _directoryService = directoryService;
            _documentService = documentService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetChildren([FromQuery] Guid directoryId)
        {
            return await Process(async () =>
            {
                var children = await _directoryService.GetChildren(directoryId);

                return Ok(children);
            });
        }

        [HttpGet]
        [Route("fileUrl")]
        public async Task<IActionResult> GetDocumentUrl([FromQuery] Guid documentId)
        {
            return await Process(async () =>
            {
                var user = await _userService.GetUserByPrincipal(User);
                var documentUrl = await _documentService.GetUrl(user.Id, documentId);

                return Ok(documentUrl);
            });
        }
    }
}