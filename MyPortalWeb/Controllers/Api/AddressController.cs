using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    public class AddressController : BaseApiController
    {
        private IAddressService _addressService;

        public AddressController(IUserService userService, IRoleService roleService, IAddressService addressService) :
            base(userService, roleService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        [Route("api/people/{personId}/addresses")]
        [Permission(PermissionRequirement.RequireAny, PermissionValue.PeopleViewContactDetails,
            PermissionValue.StudentViewStudentDetails, PermissionValue.PeopleViewAgentDetails)]
        [ProducesResponseType(typeof(IEnumerable<AddressModel>), 200)]
        public async Task<IActionResult> GetAddressesByPerson([FromRoute] Guid personId)
        {
            try
            {
                var addresses = await _addressService.GetAddressesByPerson(personId);

                return Ok(addresses);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
