using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
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
        [Route("getChildren", Name = "ApiDirectoryGetChildren")]
        public async Task<IActionResult> GetChildren([FromQuery] Guid directoryId)
        {
            return await ProcessAsync(async () =>
            {

                var user = await _userService.GetUserByPrincipal(User);

                if (await _directoryService.IsAuthorised(user, directoryId))
                {
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
                }

                return Unauthorized("Access denied.");
            });
        }

        [HttpPost]
        [Route("create", Name = "ApiDirectoryCreate")]
        public async Task<IActionResult> Create([FromForm] CreateDirectoryModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await _userService.GetUserByPrincipal(User);

                if (await _directoryService.IsAuthorised(user, model.ParentId))
                {
                    var directory = new DirectoryModel
                    {
                        ParentId = model.ParentId,
                        Name = model.Name,
                        Private = model.Private,
                        StaffOnly = model.StaffOnly
                    };

                    await _directoryService.Create(directory);

                    return Ok("Directory created successfully.");
                }

                return Unauthorized("Access denied.");
            });
        }

        [HttpPut]
        [Route("update", Name = "ApiDirectoryUpdate")]
        public async Task<IActionResult> Update([FromForm] UpdateDirectoryModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await _userService.GetUserByPrincipal(User);

                if (await _directoryService.IsAuthorised(user, model.Id))
                {
                    var directory = new DirectoryModel
                    {
                        Id = model.Id,
                        ParentId = model.ParentId,
                        Name = model.Name,
                        Private = model.Private,
                        StaffOnly = model.StaffOnly
                    };

                    await _directoryService.Update(directory);

                    return Ok("Directory was successfully updated.");
                }

                return Unauthorized("Access denied.");
            });
        }

        [HttpDelete]
        [Route("delete", Name = "ApiDirectoryDelete")]
        public async Task<IActionResult> Delete([FromQuery] Guid directoryId)
        {
            return await ProcessAsync(async () =>
            {
                var user = await _userService.GetUserByPrincipal(User);

                if (await _directoryService.IsAuthorised(user, directoryId))
                {
                    await _directoryService.Delete(directoryId);

                    return Ok("The directory was successfully deleted.");
                }

                return Unauthorized("Access denied.");
            });
        }

        [HttpGet]
        [Route("getById", Name = "ApiDirectoryGetById")]
        public async Task<IActionResult> GetById([FromQuery] Guid directoryId)
        {
            return await ProcessAsync(async () =>
            {
                var user = await _userService.GetUserByPrincipal(User);

                if (await _directoryService.IsAuthorised(user, directoryId))
                {
                    var directory = await _directoryService.GetById(directoryId);

                    return Ok(directory);
                }

                return Unauthorized("Access denied.");
            });
        }

        public override void Dispose()
        {
            _directoryService.Dispose();
            _documentService.Dispose();
        }
    }
}