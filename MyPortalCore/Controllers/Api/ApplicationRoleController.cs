using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Authorisation.Attributes;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;

namespace MyPortalCore.Controllers.Api
{ 
    [Route("api/userManagement/role")]
    public class ApplicationRoleController : BaseApiController
    {
        private readonly IApplicationRoleService _service;

        public ApplicationRoleController(IApplicationRoleService service)
        {
            _service = service;
        }

        [Authorize(Policy = PolicyDictionary.UserType.Staff)]
        [RequiresPermission(PermissionDictionary.System.Roles.Edit)]
        [Route("Search", Name = "ApiApplicationRoleSearch")]
        public async Task<IActionResult> Search([FromQuery] string roleName)
        {
            try
            {
                var roles = await _service.Get(roleName);

                var result = roles.Select(_dTMapper.Map<DataGridApplicationRole>);

                return Ok(result);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}