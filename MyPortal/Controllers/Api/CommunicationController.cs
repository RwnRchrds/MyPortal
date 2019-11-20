using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Models.Database;
using MyPortal.Services;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/communication")]
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
        [RequiresPermission("EditContactInformation")]
        public async Task<IHttpActionResult> CreateEmailAddress([FromBody] CommunicationEmailAddress emailAddress)
        {
            try
            {
                await _service.CreateEmailAddress(emailAddress);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Email address created");
        }

        [HttpPost]
        [Route("emailAddresses/update", Name = "ApiUpdateEmailAddress")]
        [RequiresPermission("EditContactInformation")]
        public async Task<IHttpActionResult> UpdateEmailAddress([FromBody] CommunicationEmailAddress emailAddress)
        {
            try
            {
                await _service.UpdateEmailAddress(emailAddress);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Email address updated");
        }

        [HttpDelete]
        [Route("emailAddresses/delete/{emailAddressId:int}", Name = "ApiDeleteEmailAddress")]
        [RequiresPermission("EditContactInformation")]
        public async Task<IHttpActionResult> DeleteEmailAddress([FromUri] int emailAddressId)
        {
            try
            {
                await _service.DeleteEmailAddress(emailAddressId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Email address deleted");
        }
    }
}
