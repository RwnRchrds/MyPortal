using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/staff")]
    public class StaffController : MyPortalApiController
    {
        private readonly StaffMemberService _service;

        public StaffController()
        {
            _service = new StaffMemberService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }

        [HttpPost]
        [RequiresPermission("EditStaff")]
        [Route("create", Name = "ApiCreateStaff")]
        public async Task<IHttpActionResult> CreateStaff([FromBody] StaffMemberDto staffMember)
        {
            try
            {
                await _service.CreateStaffMember(staffMember);
                
                return Ok("Staff member created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditStaff")]
        [Route("delete/{staffMemberId:int}", Name = "ApiDeleteStaffMember")]
        public async Task<IHttpActionResult> DeleteStaff([FromUri] int staffMemberId)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                var currentUser = await _service.GetStaffMemberByUserId(userId);

                if (currentUser.Id == staffMemberId)
                {
                    return Content(HttpStatusCode.BadRequest, "Cannot delete current user.");
                }

                await _service.DeleteStaffMember(staffMemberId);

                return Ok("Staff member deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditStaff")]
        [Route("update", Name = "ApiUpdateStaffMember")]
        public async Task<IHttpActionResult> UpdateStaffMember([FromBody] StaffMemberDto staffMember)
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
        [Route("get/all", Name = "ApiGetAllStaffMembers")]
        public async Task<IEnumerable<StaffMemberDto>> GetAllStaffMembers()
        {
            try
            {
                return await _service.GetAllStaffMembers();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [Route("get/dataGrid/all", Name = "ApiGetAllStaffMembersDataGrid")]
        [RequiresPermission("ViewStaff")]
        public async Task<IHttpActionResult> GetAllStaffMembersDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var staff = await _service.GetAllStaffMembers();

                var list = staff.Select(_mapping.Map<DataGridStaffMemberDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [Route("get/byId/{staffMemberId:int}", Name = "ApiGetStaffMemberById")]
        [RequiresPermission("ViewStaff")]
        public async Task<StaffMemberDto> GetStaffMemberById([FromUri] int staffMemberId)
        {
            try
            {
                return await _service.GetStaffMemberById(staffMemberId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
    }
}