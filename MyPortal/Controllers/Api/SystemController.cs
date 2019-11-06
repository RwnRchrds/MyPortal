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
        
        [HttpPost]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/create", Name = "ApiSystemCreateBulletin")]
        public async Task<IHttpActionResult> CreateBulletin([FromBody] SystemBulletin bulletin)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                var autoApprove = await User.HasPermissionAsync("ApproveBulletins");

                await _service.CreateBulletin(bulletin, userId, autoApprove);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Bulletin created");
        }

        [HttpPost]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/update", Name = "ApiSystemUpdateBulletin")]
        public async Task<IHttpActionResult> UpdateBulletin([FromBody] SystemBulletin bulletin)
        {
            try
            {
                await _service.UpdateBulletin(bulletin);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Bulletin updated");
        }

        [HttpDelete]
        [RequiresPermission("EditBulletins")]
        [Route("bulletins/delete/{bulletinId:int}", Name = "ApiSystemDeleteBulletin")]
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
        [Route("bulletins/get/all", Name = "ApiSystemGetAllBulletins")]
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
        [RequiresPermission("ViewStaffBulletins")]
        [Route("bulletins/get/approved", Name = "ApiSystemGetApprovedBulletins")]
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
        [RequiresPermission("ViewStaffBulletins")]
        [Route("bulletins/get/own", Name = "ApiSystemGetOwnBulletins")]
        public async Task<IEnumerable<SystemBulletinDto>> GetOwnBulletins()
        {
            try
            {
                using (var staffService = new StaffMemberService(UnitOfWork))
                {
                    var userId = User.Identity.GetUserId();

                    var staff = await staffService.GetStaffMemberFromUserId(userId);
                    
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
        [Route("bulletins/get/student", Name = "ApiSystemGetStudentBulletins")]
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