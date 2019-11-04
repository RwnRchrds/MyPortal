using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Models.Database;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/people/staff")]
    public class StaffController : MyPortalApiController
    {
        [HttpPost]
        [RequiresPermission("EditStaff")]
        [Route("create", Name = "ApiPeopleCreateStaff")]
        public async Task<IHttpActionResult> CreateStaff([FromBody] StaffMember staffMember)
        {
            try
            {
                await StaffMemberService.CreateStaffMember(staffMember, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Staff member created");
        }

        [HttpDelete]
        [RequiresPermission("EditStaff")]
        [Route("delete/{staffMemberId:int}", Name = "ApiPeopleDeleteStaffMember")]
        public async Task<IHttpActionResult> DeleteStaff([FromUri] int staffMemberId)
        {
            var userId = User.Identity.GetUserId();

            try
            {
                await StaffMemberService.DeleteStaffMember(staffMemberId, userId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Staff member deleted");
        }

        [HttpPost]
        [RequiresPermission("EditStaff")]
        [Route("update", Name = "ApiPeopleUpdateStaffMember")]
        public async Task<IHttpActionResult> UpdateStaffMember([FromBody] StaffMember staffMember)
        {
            try
            {
                await StaffMemberService.UpdateStaffMember(staffMember, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Staff member updated");
        }

        [HttpGet]
        [RequiresPermission("ViewStaff")]
        [Route("get/all", Name = "ApiPeopleGetAllStaffMembers")]
        public async Task<IEnumerable<StaffMemberDto>> GetAllStaffMembers()
        {
            try
            {
                return await StaffMemberService.GetAllStaffMembers(_context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [Route("get/dataGrid/all", Name = "ApiPeopleGetAllStaffMembersDataGrid")]
        [RequiresPermission("ViewStaff")]
        public async Task<IHttpActionResult> GetAllStaffMembersDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var staff = await StaffMemberService.GetAllStaffMembersDataGrid(_context);

                return PrepareDataGridObject(staff, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [Route("get/byId/{staffMemberId:int}", Name = "ApiPeopleGetStaffMemberById")]
        [RequiresPermission("ViewStaff")]
        public async Task<StaffMemberDto> GetStaffMemberById([FromUri] int staffMemberId)
        {
            try
            {
                return await StaffMemberService.GetStaffMemberById(staffMemberId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
    }
}