using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MyPortal.Models.Attributes;
using MyPortal.Models.Database;
using MyPortal.Processes;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/communication")]
    public class CommunicationController : MyPortalApiController
    {
        [HttpPost]
        [Route("emailAddresses/create")]
        [RequiresPermission("EditContactInformation")]
        public async Task<IHttpActionResult> CreateEmailAddress([FromBody] CommunicationEmailAddress emailAddress)
        {
            try
            {
                await CommunicationProcesses.CreateEmailAddress(emailAddress, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Email address created");
        }
    }
}
