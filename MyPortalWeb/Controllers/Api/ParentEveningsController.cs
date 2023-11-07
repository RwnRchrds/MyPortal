using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces.Services;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Microsoft.AspNetCore.Components.Route("api/parentEvenings")]
    public class ParentEveningsController : BaseApiController
    {
        private readonly IParentEveningService _parentEveningService;

        public ParentEveningsController(IUserService userService, IParentEveningService parentEveningService)
            : base(userService)
        {
            _parentEveningService = parentEveningService;
        }

        [HttpGet]
        [Route("templates/{parentEveningId}/{staffMemberId}")]
        public async Task<IActionResult> GetParentEveningTemplatesByStaffMember([FromRoute] Guid parentEveningId,
            [FromRoute] Guid staffMemberId)
        {
            try
            {
                var parentEveningTemplates =
                    await _parentEveningService.GetAppointmentTemplatesByStaffMember(parentEveningId, staffMemberId);

                return Ok(parentEveningTemplates);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}