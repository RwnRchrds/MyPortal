using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using MyPortal.Logic.Models.Response.Documents;
using MyPortal.Logic.Models.Summary;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/document/directories")]
    public class DirectoriesController : BaseApiController
    {
        private IDirectoryService _directoryService;

        public DirectoriesController(IUserService userService, IRoleService roleService,
            IDirectoryService directoryService) : base(userService, roleService)
        {
            _directoryService = directoryService;
        }

        [HttpGet]
        [Route("children")]
        [ProducesResponseType(typeof(DirectoryChildListWrapper), 200)]
        public async Task<IActionResult> GetChildren([FromQuery] Guid directoryId)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);

                if (await _directoryService.IsAuthorised(user, directoryId))
                {
                    var directory = await _directoryService.GetById(directoryId);

                    var userIsStaff = user.UserType == UserTypes.Staff;

                    var children = await _directoryService.GetChildren(directoryId, userIsStaff);

                    var childList = new List<DirectoryChildSummaryModel>();

                    childList.AddRange(children.Subdirectories.Select(x => x.GetListModel()));
                    childList.AddRange(children.Files.Select(x => x.GetListModel()));

                    var response = new DirectoryChildListWrapper
                    {
                        Directory = directory,
                        Children = childList
                    };

                    return Ok(response);
                }

                return Forbid("You do not have permission to access this directory.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateDirectoryModel model)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);

                if (await _directoryService.IsAuthorised(user, model.ParentId))
                {
                    var directory = new CreateDirectoryModel
                    {
                        ParentId = model.ParentId,
                        Name = model.Name,
                        Restricted = model.Restricted
                    };

                    await _directoryService.Create(directory);

                    return Ok();
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateDirectoryModel model)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);

                if (await _directoryService.IsAuthorised(user, model.Id))
                {
                    var directory = new UpdateDirectoryModel
                    {
                        Id = model.Id,
                        ParentId = model.ParentId,
                        Name = model.Name,
                        Restricted = model.Restricted
                    };

                    await _directoryService.Update(directory);

                    return Ok();
                }

                return Unauthorized();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] Guid directoryId)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);

                if (await _directoryService.IsAuthorised(user, directoryId))
                {
                    await _directoryService.Delete(directoryId);

                    return Ok();
                }

                return Unauthorized();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(typeof(DirectoryModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid directoryId)
        {
            try
            {
                var user = await UserService.GetUserByPrincipal(User);

                if (await _directoryService.IsAuthorised(user, directoryId))
                {
                    var directory = _directoryService.GetById(directoryId);

                    return Ok(directory);
                }

                return Unauthorized("Access denied.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
