using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Models.Database;
using MyPortal.Services;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/system")]
    public class SystemController : MyPortalApiController
    {
        private readonly SystemService _service;

        public SystemController()
        {
            _service = new SystemService(UnitOfWork);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
        
        [HttpPost]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/create", Name = "ApiCreateBulletin")]
        public async Task<IHttpActionResult> CreateBulletin([FromBody] SystemBulletin bulletin)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                var approvePermissions = await User.HasPermissionAsync("ApproveBulletins");

                await _service.CreateBulletin(bulletin, userId, approvePermissions);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Bulletin created");
        }

        [HttpPost]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/update", Name = "ApiUpdateBulletin")]
        public async Task<IHttpActionResult> UpdateBulletin([FromBody] SystemBulletin bulletin)
        {
            try
            {
                var approvePermissions = await User.HasPermissionAsync("ApproveBulletins");
                await _service.UpdateBulletin(bulletin, approvePermissions);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Bulletin updated");
        }

        [HttpDelete]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/delete/{bulletinId:int}", Name = "ApiDeleteBulletin")]
        public async Task<IHttpActionResult> DeleteBulletin([FromUri] int bulletinId)
        {
            try
            {
                await _service.DeleteBulletin(bulletinId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Bulletin deleted");
        }

        [HttpGet]
        [RequiresPermission("ApproveBulletins")]
        [Route("bulletins/get/all", Name = "ApiGetAllBulletins")]
        public async Task<IEnumerable<SystemBulletinDto>> GetAllBulletins()
        {
            try
            {
                var bulletins = await _service.GetAllBulletins();

                return bulletins.Select(Mapper.Map<SystemBulletin, SystemBulletinDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewApprovedBulletins")]
        [Route("bulletins/get/approved", Name = "ApiGetApprovedBulletins")]
        public async Task<IEnumerable<SystemBulletinDto>> GetApprovedBulletins()
        {
            try
            {
                var bulletins = await _service.GetApprovedBulletins();

                return bulletins.Select(Mapper.Map<SystemBulletin, SystemBulletinDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewApprovedBulletins")]
        [Route("bulletins/get/own", Name = "ApiGetOwnBulletins")]
        public async Task<IEnumerable<SystemBulletinDto>> GetOwnBulletins()
        {
            try
            {
                using (var staffService = new StaffMemberService(UnitOfWork))
                {
                    var userId = User.Identity.GetUserId();

                    var staff = await staffService.GetStaffMemberByUserId(userId);
                    
                    var bulletins = await _service.GetOwnBulletins(staff.Id);

                    return bulletins.Select(Mapper.Map<SystemBulletin, SystemBulletinDto>);
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
        public async Task<IEnumerable<SystemBulletinDto>> GetStudentBulletins()
        {
            try
            {
                var bulletins = await _service.GetApprovedStudentBulletins();

                return bulletins.Select(Mapper.Map<SystemBulletin, SystemBulletinDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
    }
}