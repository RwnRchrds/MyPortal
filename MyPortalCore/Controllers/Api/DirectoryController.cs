using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.ListModels;

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
        [Route("list", Name = "ApiDirectoryListChildren")]
        public async Task<IActionResult> GetChildren([FromQuery] Guid directoryId)
        {
            return await Process(async () =>
            {
                var children = await _directoryService.GetChildren(directoryId);

                var childList = new List<DirectoryChildListModel>();

                childList.AddRange(children.Subdirectories.Select(x => x.GetListModel()));
                childList.AddRange(children.Files.Select(x => x.GetListModel()));

                return Ok(childList);
            });
        }

        public override void Dispose()
        {
            _directoryService.Dispose();
            _documentService.Dispose();
        }
    }
}