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
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/people/staff")]
    public class StaffController : MyPortalApiController
    {
        private readonly StaffMemberService _service;

        public StaffController()
        {
            _service = new StaffMemberService(UnitOfWork);
        }

        [HttpPost]
        [RequiresPermission("EditStaff")]
        [Route("create", Name = "ApiPeopleCreateStaff")]
        public async Task<IHttpActionResult> CreateStaff([FromBody] StaffMember staffMember)
        {
            try
            {
                await _service.CreateStaffMember(staffMember);
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
            try
            {
                var userId = User.Identity.GetUserId();
                await _service.DeleteStaffMember(staffMemberId, userId);
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
                await _service.UpdateStaffMember(staffMember);
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
                var staff = await _service.GetAllStaffMembers();

                return staff.Select(Mapper.Map<StaffMember, StaffMemberDto>);
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
                var staff = await _service.GetAllStaffMembers();

                var list = staff.Select(Mapper.Map<StaffMember, GridStaffMemberDto>);

                return PrepareDataGridObject(list, dm);
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
                var staff = await _service.GetStaffMemberById(staffMemberId);

                return Mapper.Map<StaffMember, StaffMemberDto>(staff);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
    }
}