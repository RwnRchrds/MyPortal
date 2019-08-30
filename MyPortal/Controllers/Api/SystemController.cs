using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models.Attributes;
using MyPortal.Models.Database;
using MyPortal.Processes;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/system")]
    public class SystemController : MyPortalApiController
    {
        [HttpPost]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/create")]
        public async Task<IHttpActionResult> CreateBulletin([FromBody] SystemBulletin bulletin)
        {
            var userId = User.Identity.GetUserId();

            var autoApprove = User.HasPermission("ApproveBulletins");

            var result = await SystemProcesses.CreateBulletin(bulletin, userId, _context, autoApprove);

            return PrepareResponse(result);
        }

        [HttpPost]
        [RequiresPermission("EditBulletins")]
        public IHttpActionResult UpdateBulletin([FromBody] SystemBulletin bulletin)
        {
            var approvable = User.HasPermission("ApproveBulletins");

            return PrepareResponse(SystemProcesses.UpdateBulletin(bulletin, _context, approvable));
        }

        [HttpDelete]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/delete/{bulletinId:int}")]
        public IHttpActionResult DeleteBulletin([FromUri] int bulletinId)
        {
            return PrepareResponse(SystemProcesses.DeleteBulletin(bulletinId, _context));
        }

        [HttpGet]
        [RequiresPermission("ApproveBulletins")]
        [Route("bulletins/get/all")]
        public IEnumerable<SystemBulletinDto> GetAllBulletins()
        {
            return PrepareResponseObject(SystemProcesses.GetAllBulletins(_context));
        }

        [HttpGet]
        [RequiresPermission("ViewStaffBulletins")]
        [Route("bulletins/get/approved")]
        public IEnumerable<SystemBulletinDto> GetApprovedBulletins()
        {
            return PrepareResponseObject(SystemProcesses.GetApprovedBulletins(_context));
        }

        [HttpGet]
        [RequiresPermission("ViewStaffBulletins")]
        [Route("bulletins/get/own")]
        public IEnumerable<SystemBulletinDto> GetOwnBulletins()
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponseObject(SystemProcesses.GetOwnBulletins(userId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudentBulletins")]
        [Route("bulletins/get/student")]
        public IEnumerable<SystemBulletinDto> GetStudentBulletins()
        {
            return PrepareResponseObject(SystemProcesses.GetApprovedStudentBulletins(_context));
        }
    }
}