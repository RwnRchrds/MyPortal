using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Contacts;
using MyPortal.Logic.Models.Requests.Addresses;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    public class AddressController : BaseApiController
    {
        private readonly IAddressService _addressService;

        public AddressController(IUserService userService, IAddressService addressService) : base(userService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        [Route("api/people/{personId}/addresses")]
        public async Task<IActionResult> CreatePersonAddress([FromQuery] bool forceCreate,
            [FromBody] EntityAddressRequestModel personAddress)
        {
            try
            {
                if (!forceCreate)
                {
                    var searchOptions = personAddress.GetSearchOptions();
                    var existingAddresses = await _addressService.GetMatchingAddresses(searchOptions);

                    if (existingAddresses.Any())
                    {
                        return Ok(existingAddresses);
                    }
                }
                
                //TODO: Create address

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
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
