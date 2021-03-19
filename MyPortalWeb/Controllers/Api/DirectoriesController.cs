using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using MyPortal.Logic.Models.Response.Documents;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/document/directories")]
    public class DirectoriesController : BaseApiController
    {
        

        [HttpGet]
        [Route("children")]
        [ProducesResponseType(typeof(DirectoryChildListWrapper), 200)]
        public async Task<IActionResult> GetChildren([FromQuery] Guid directoryId)
        {
            return await ProcessAsync(async () =>
            {

                var user = await Services.Users.GetUserByPrincipal(User);

                if (await Services.Directories.IsAuthorised(user, directoryId))
                {
                    var directory = await Services.Directories.GetById(directoryId);

                    var userIsStaff = user.UserType == UserTypes.Staff;

                    var children = await Services.Directories.GetChildren(directoryId, userIsStaff);

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
        [Route("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateDirectoryModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await Services.Users.GetUserByPrincipal(User);

                if (await Services.Directories.IsAuthorised(user, model.ParentId))
                {
                    var directory = new DirectoryModel
                    {
                        ParentId = model.ParentId,
                        Name = model.Name,
                        Private = model.Private,
                        StaffOnly = model.StaffOnly
                    };

                    await Services.Directories.Create(directory);

                    return Ok();
                }

                return Unauthorized();
            });
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateDirectoryModel model)
        {
            return await ProcessAsync(async () =>
            {
                var user = await Services.Users.GetUserByPrincipal(User);

                if (await Services.Directories.IsAuthorised(user, model.Id))
                {
                    var directory = new DirectoryModel
                    {
                        Id = model.Id,
                        ParentId = model.ParentId,
                        Name = model.Name,
                        Private = model.Private,
                        StaffOnly = model.StaffOnly
                    };

                    await Services.Directories.Update(directory);

                    return Ok();
                }

                return Unauthorized();
            });
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] Guid directoryId)
        {
            return await ProcessAsync(async () =>
            {
                var user = await Services.Users.GetUserByPrincipal(User);

                if (await Services.Directories.IsAuthorised(user, directoryId))
                {
                    await Services.Directories.Delete(directoryId);

                    return Ok();
                }

                return Unauthorized();
            });
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(typeof(DirectoryModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid directoryId)
        {
            return await ProcessAsync(async () =>
            {
                var user = await Services.Users.GetUserByPrincipal(User);

                if (await Services.Directories.IsAuthorised(user, directoryId))
                {
                    var directory = await Services.Directories.GetById(directoryId);

                    return Ok(directory);
                }

                return Unauthorized("Access denied.");
            });
        }

        public DirectoriesController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
        {
        }
    }
}
