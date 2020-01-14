using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Services;
using MyPortal.BusinessLogic.Services.Identity;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/system")]
    public class SystemController : MyPortalApiController
    {
        private readonly SystemService _service;

        public SystemController()
        {
            _service = new SystemService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }

        [HttpPost]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/create", Name = "ApiCreateBulletin")]
        public async Task<IHttpActionResult> CreateBulletin([FromBody] BulletinDto bulletin)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                var approvePermissions = await User.HasPermissionAsync("ApproveBulletins");

                await _service.CreateBulletin(bulletin, userId, approvePermissions);
                await _service.SaveChanges();

                return Ok("Bulletin created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/update", Name = "ApiUpdateBulletin")]
        public async Task<IHttpActionResult> UpdateBulletin([FromBody] BulletinDto bulletin)
        {
            try
            {
                var approvePermissions = await User.HasPermissionAsync("ApproveBulletins");
                await _service.UpdateBulletin(bulletin, approvePermissions);
                await _service.SaveChanges();

                return Ok("Bulletin updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/delete/{bulletinId:int}", Name = "ApiDeleteBulletin")]
        public async Task<IHttpActionResult> DeleteBulletin([FromUri] int bulletinId)
        {
            try
            {
                await _service.DeleteBulletin(bulletinId);
                await _service.SaveChanges();

                return Ok("Bulletin deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ApproveBulletins")]
        [Route("bulletins/get/all", Name = "ApiGetAllBulletins")]
        public async Task<IEnumerable<BulletinDto>> GetAllBulletins()
        {
            try
            {
                return await _service.GetAllBulletins();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewApprovedBulletins")]
        [Route("bulletins/get/approved", Name = "ApiGetApprovedBulletins")]
        public async Task<IEnumerable<BulletinDto>> GetApprovedBulletins()
        {
            try
            {
                return await _service.GetApprovedBulletins();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewApprovedBulletins")]
        [Route("bulletins/get/own", Name = "ApiGetOwnBulletins")]
        public async Task<IEnumerable<BulletinDto>> GetOwnBulletins()
        {
            try
            {
                using (var staffService = new StaffMemberService())
                {
                    var userId = User.Identity.GetUserId();

                    var staff = await staffService.GetStaffMemberByUserId(userId);
                    
                    return await _service.GetOwnBulletins(staff.Id);
                }
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewStudentBulletins")]
        [Route("bulletins/get/student", Name = "ApiGetStudentBulletins")]
        public async Task<IEnumerable<BulletinDto>> GetStudentBulletins()
        {
            try
            {
                return await _service.GetApprovedStudentBulletins();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
    }
}