using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MyPortal.Attributes;
using MyPortal.Attributes.Filters;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/communication")]
    [ValidateModel]
    public class CommunicationController : MyPortalApiController
    {
        private readonly CommunicationService _service;

        public CommunicationController()
        {
            _service = new CommunicationService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
        
        [HttpPost]
        [Route("emailAddresses/create", Name = "ApiCreateEmailAddress")]
        [RequiresPermission("EditContacts")]
        public async Task<IHttpActionResult> CreateEmailAddress([FromBody] EmailAddressDto emailAddress)
        {
            try
            {
                await _service.CreateEmailAddress(emailAddress);
                await _service.SaveChanges();

                return Ok("Email address created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("emailAddresses/update", Name = "ApiUpdateEmailAddress")]
        [RequiresPermission("EditContacts")]
        public async Task<IHttpActionResult> UpdateEmailAddress([FromBody] EmailAddressDto emailAddress)
        {
            try
            {
                await _service.UpdateEmailAddress(emailAddress);
                await _service.SaveChanges();

                return Ok("Email address updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("emailAddresses/delete/{emailAddressId:int}", Name = "ApiDeleteEmailAddress")]
        [RequiresPermission("EditContacts")]
        public async Task<IHttpActionResult> DeleteEmailAddress([FromUri] int emailAddressId)
        {
            try
            {
                await _service.DeleteEmailAddress(emailAddressId);
                await _service.SaveChanges();

                return Ok("Email address deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("phoneNumbers/create", Name = "ApiCreatePhoneNumber")]
        [RequiresPermission("EditContacts")]
        public async Task<IHttpActionResult> CreatePhoneNumber(PhoneNumberDto phoneNumber)
        {
            try
            {
                await _service.CreatePhoneNumber(phoneNumber);
                await _service.SaveChanges();

                return Ok("Phone number created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("phoneNumbers/update", Name = "ApiUpdatePhoneNumber")]
        [RequiresPermission("EditContacts")]
        public async Task<IHttpActionResult> UpdatePhoneNumber(PhoneNumberDto phoneNumber)
        {
            try
            {
                await _service.UpdatePhoneNumber(phoneNumber);
                await _service.SaveChanges();

                return Ok("Phone number updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewContacts")]
        [Route("phoneNumbers/get/dataGrid/byPerson/{personId:int}", Name = "ApiGetPhoneNumbersByPersonDataGrid")]
        public async Task<IHttpActionResult> GetPhoneNumbersByPerson([FromUri] int personId, [FromBody] DataManagerRequest dm)
        {
            try
            {
                var phoneNumbers = await _service.GetPhoneNumbersByPerson(personId);

                var list = phoneNumbers.Select(_mapper.Map<DataGridPhoneNumberDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewContacts")]
        [Route("emailAddresses/get/dataGrid/byPerson/{personId:int}", Name = "ApiGetEmailAddressesByPersonDataGrid")]
        public async Task<IHttpActionResult> GetEmailAddressesByPerson([FromUri] int personId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var emailAddresses = await _service.GetEmailAddressesByPerson(personId);

                var list = emailAddresses.Select(
                    _mapper.Map<DataGridEmailAddressDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
