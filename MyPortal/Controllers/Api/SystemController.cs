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
        [Route("bulletins/create", Name = "ApiSystemCreateBulletin")]
        public async Task<IHttpActionResult> CreateBulletin([FromBody] SystemBulletin bulletin)
        {
            var userId = User.Identity.GetUserId();

            var autoApprove = await User.HasPermissionAsync("ApproveBulletins");

            var result = await SystemProcesses.CreateBulletin(bulletin, userId, _context, autoApprove);

            return PrepareResponse(result);
        }

        [HttpPost]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/update", Name = "ApiSystemUpdateBulletin")]
        public async Task<IHttpActionResult> UpdateBulletin([FromBody] SystemBulletin bulletin)
        {
            var approvable = await User.HasPermissionAsync("ApproveBulletins");

            return PrepareResponse(SystemProcesses.UpdateBulletin(bulletin, _context, approvable));
        }

        [HttpDelete]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/delete/{bulletinId:int}", Name = "ApiSystemDeleteBulletin")]
        public IHttpActionResult DeleteBulletin([FromUri] int bulletinId)
        {
            return PrepareResponse(SystemProcesses.DeleteBulletin(bulletinId, _context));
        }

        [HttpGet]
        [RequiresPermission("ApproveBulletins")]
        [Route("bulletins/get/all", Name = "ApiSystemGetAllBulletins")]
        public IEnumerable<SystemBulletinDto> GetAllBulletins()
        {
            return PrepareResponseObject(SystemProcesses.GetAllBulletins(_context));
        }

        [HttpGet]
        [RequiresPermission("ViewStaffBulletins")]
        [Route("bulletins/get/approved", Name = "ApiSystemGetApprovedBulletins")]
        public IEnumerable<SystemBulletinDto> GetApprovedBulletins()
        {
            return PrepareResponseObject(SystemProcesses.GetApprovedBulletins(_context));
        }

        [HttpGet]
        [RequiresPermission("ViewStaffBulletins")]
        [Route("bulletins/get/own", Name = "ApiSystemGetOwnBulletins")]
        public IEnumerable<SystemBulletinDto> GetOwnBulletins()
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponseObject(SystemProcesses.GetOwnBulletins(userId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudentBulletins")]
        [Route("bulletins/get/student", Name = "ApiSystemGetStudentBulletins")]
        public IEnumerable<SystemBulletinDto> GetStudentBulletins()
        {
            return PrepareResponseObject(SystemProcesses.GetApprovedStudentBulletins(_context));
        }
    }
}