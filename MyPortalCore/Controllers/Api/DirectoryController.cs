using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Authorisation.Attributes;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.ListModels;
using MyPortal.Logic.Models.Requests.Documents;

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
        [Route("list", Name = "ApiDirectoryGetChildren")]
        [Authorize(Policy = Policies.UserType.Staff)]
        public async Task<IActionResult> GetChildren([FromQuery] Guid directoryId)
        {
            return await Process(async () =>
            {
                var user = await _userService.GetUserByPrincipal(User);

                var directory = await _directoryService.GetById(directoryId);

                var userIsStaff = user.UserType == UserTypes.Staff;

                var children = await _directoryService.GetChildren(directoryId, userIsStaff);

                var childList = new List<DirectoryChildListModel>();

                childList.AddRange(children.Subdirectories.Select(x => x.GetListModel()));
                childList.AddRange(children.Files.Select(x => x.GetListModel()));

                var response = new DirectoryChildListWrapper
                {
                    Directory = directory,
                    Children = childList
                };

                return Ok(response);
            });
        }

        public override void Dispose()
        {
            _directoryService.Dispose();
            _documentService.Dispose();
        }
    }
}