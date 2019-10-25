using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Models.Database;
using MyPortal.Services;

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

            var result = await SystemService.CreateBulletin(bulletin, userId, _context, autoApprove);

            return PrepareResponse(result);
        }

        [HttpPost]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/update", Name = "ApiSystemUpdateBulletin")]
        public async Task<IHttpActionResult> UpdateBulletin([FromBody] SystemBulletin bulletin)
        {
            var approvable = await User.HasPermissionAsync("ApproveBulletins");

            return PrepareResponse(SystemService.UpdateBulletin(bulletin, _context, approvable));
        }

        [HttpDelete]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/delete/{bulletinId:int}", Name = "ApiSystemDeleteBulletin")]
        public IHttpActionResult DeleteBulletin([FromUri] int bulletinId)
        {
            return PrepareResponse(SystemService.DeleteBulletin(bulletinId, _context));
        }

        [HttpGet]
        [RequiresPermission("ApproveBulletins")]
        [Route("bulletins/get/all", Name = "ApiSystemGetAllBulletins")]
        public IEnumerable<SystemBulletinDto> GetAllBulletins()
        {
            return PrepareResponseObject(SystemService.GetAllBulletins(_context));
        }

        [HttpGet]
        [RequiresPermission("ViewStaffBulletins")]
        [Route("bulletins/get/approved", Name = "ApiSystemGetApprovedBulletins")]
        public IEnumerable<SystemBulletinDto> GetApprovedBulletins()
        {
            return PrepareResponseObject(SystemService.GetApprovedBulletins(_context));
        }

        [HttpGet]
        [RequiresPermission("ViewStaffBulletins")]
        [Route("bulletins/get/own", Name = "ApiSystemGetOwnBulletins")]
        public IEnumerable<SystemBulletinDto> GetOwnBulletins()
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponseObject(SystemService.GetOwnBulletins(userId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStudentBulletins")]
        [Route("bulletins/get/student", Name = "ApiSystemGetStudentBulletins")]
        public IEnumerable<SystemBulletinDto> GetStudentBulletins()
        {
            return PrepareResponseObject(SystemService.GetApprovedStudentBulletins(_context));
        }
    }
}