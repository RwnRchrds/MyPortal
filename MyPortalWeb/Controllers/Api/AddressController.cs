using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Addresses;
using MyPortal.Logic.Models.Requests.Addresses;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    public class AddressController : BaseApiController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        [Route("api/addresses")]
        [Permission(PermissionRequirement.RequireAny, PermissionValue.PeopleEditContactDetails,
            PermissionValue.StudentEditStudentDetails, PermissionValue.PeopleEditAgentDetails,
            PermissionValue.PeopleEditStaffBasicDetails, PermissionValue.AgencyEditAgencies)]
        public async Task<IActionResult> GetExistingAddresses([FromQuery] AddressSearchRequestModel searchModel)
        {
            try
            {
                var addresses = await _addressService.GetMatchingAddresses(searchModel);

                return Ok(addresses);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("api/addresses/{addressId}")]
        [Permission(PermissionRequirement.RequireAny, PermissionValue.PeopleEditContactDetails,
            PermissionValue.StudentEditStudentDetails, PermissionValue.PeopleEditAgentDetails,
            PermissionValue.PeopleEditStaffBasicDetails, PermissionValue.AgencyEditAgencies)]
        public async Task<IActionResult> UpdateAddress([FromRoute] Guid addressId, AddressRequestModel model)
        {
            try
            {
                await _addressService.UpdateAddress(addressId, model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("api/people/{personId}/addresses")]
        [Permission(PermissionRequirement.RequireAny, PermissionValue.PeopleEditContactDetails,
            PermissionValue.StudentEditStudentDetails, PermissionValue.PeopleEditAgentDetails,
            PermissionValue.PeopleEditStaffBasicDetails)]
        public async Task<IActionResult> CreatePersonAddress([FromRoute] Guid personId,
            [FromBody] EntityAddressRequestModel personAddress)
        {
            try
            {
                await _addressService.CreateAddressForPerson(personId, personAddress);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("api/people/{personId}/addresses/{addressLinkId}")]
        [Permission(PermissionRequirement.RequireAny, PermissionValue.PeopleEditContactDetails,
            PermissionValue.StudentEditStudentDetails, PermissionValue.PeopleEditAgentDetails,
            PermissionValue.PeopleEditStaffBasicDetails)]
        public async Task<IActionResult> UpdatePersonAddressLink([FromRoute] Guid addressPersonId,
            [FromBody] LinkAddressRequestModel addressLink)
        {
            try
            {
                await _addressService.UpdateAddressLinkForPerson(addressPersonId, addressLink);

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
            PermissionValue.StudentViewStudentDetails, PermissionValue.PeopleViewAgentDetails,
            PermissionValue.PeopleViewStaffBasicDetails)]
        [ProducesResponseType(typeof(IEnumerable<AddressLinkDataModel>), 200)]
        public async Task<IActionResult> GetAddressesByPerson([FromRoute] Guid personId)
        {
            try
            {
                var addresses = await _addressService.GetAddressLinksByPerson(personId);

                return Ok(addresses);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}