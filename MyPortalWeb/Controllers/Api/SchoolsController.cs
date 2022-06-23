using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.School.Bulletins;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;
using MyPortalWeb.Models.Response;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/schools")]
    public class SchoolsController : BaseApiController
    {
        private ISchoolService _schoolService;

        public SchoolsController(IUserService userService, IRoleService roleService, ISchoolService schoolService) :
            base(userService, roleService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("local/name")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetLocalSchoolName()
        {
            try
            {
                var schoolName = await _schoolService.GetLocalSchoolName();

                return Ok(new StringResponseModel(schoolName));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("local/bulletins")]
        [ProducesResponseType(typeof(IEnumerable<BulletinModel>), 200)]
        public async Task<IActionResult> GetSchoolBulletins([FromQuery] BulletinSearchOptions searchOptions)
        {
            try
            {
                if (!User.IsType(UserTypes.Staff))
                {
                    searchOptions.IncludeStaffOnly = false;
                    searchOptions.IncludeUnapproved = false;
                    searchOptions.IncludeExpired = false;
                }
                if (!await UserHasPermission(PermissionValue.SchoolApproveSchoolBulletins))
                {
                    searchOptions.IncludeUnapproved = false;
                }

                if (searchOptions.IncludeCreatedBy.HasValue)
                {
                    var user = await GetLoggedInUser();
                    searchOptions.IncludeCreatedBy = user.Id.Value;
                }

                var bulletins = await _schoolService.GetBulletins(searchOptions);

                return Ok(bulletins);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("local/bulletins/create")]
        [Permission(PermissionValue.SchoolEditSchoolBulletins)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateBulletin([FromBody] CreateBulletinRequestModel model)
        {
            try
            {
                await _schoolService.CreateBulletin(model);
                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("local/bulletins/update")]
        [Permission(PermissionValue.SchoolEditSchoolBulletins)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateBulletin([FromBody] UpdateBulletinRequestModel model)
        {
            try
            {
                await _schoolService.UpdateBulletin(model);
                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("local/bulletins/approve")]
        [Permission(PermissionValue.SchoolApproveSchoolBulletins)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ApproveBulletin([FromBody] ApproveBulletinRequestModel model)
        {
            try
            {
                await _schoolService.SetBulletinApproved(model);
                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("local/bulletins/delete/{bulletinId}")]
        [Permission(PermissionValue.SchoolEditSchoolBulletins)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteBulletin([FromRoute] Guid bulletinId)
        {
            try
            {
                await _schoolService.DeleteBulletin(bulletinId);
                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
