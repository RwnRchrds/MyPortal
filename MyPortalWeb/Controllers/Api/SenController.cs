using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/send")]
    public class SenController : BaseApiController
    {
        private ISenService _senService;

        public SenController(IUserService userService, IRoleService roleService, ISenService senService) : base(
            userService, roleService)
        {
            _senService = senService;
        }

        [HttpGet]
        [Route("giftedTalented/student/{studentId}")]
        [Permission(PermissionValue.StudentViewSenDetails)]
        [ProducesResponseType(typeof(IEnumerable<GiftedTalentedModel>), 200)]
        public async Task<IActionResult> GetGiftedTalentedByStudent([FromRoute] Guid studentId)
        {
            try
            {
                var giftedTalented = await _senService.GetGiftedTalentedSubjects(studentId);

                return Ok(giftedTalented);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
