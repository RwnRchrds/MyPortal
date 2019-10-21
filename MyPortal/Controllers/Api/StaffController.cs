using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models.Attributes;
using MyPortal.Models.Database;
using MyPortal.Processes;
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
                await PeopleProcesses.CreateStaffMember(staffMember, _context);
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
                await PeopleProcesses.DeleteStaffMember(staffMemberId, userId, _context);
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
        public IHttpActionResult UpdateStaffMember([FromBody] StaffMember staffMember)
        {
            return PrepareResponse(PeopleProcesses.UpdateStaffMember(staffMember, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewStaff")]
        [Route("get/all", Name = "ApiPeopleGetAllStaffMembers")]
        public IEnumerable<StaffMemberDto> GetAllStaffMembers()
        {
            return PrepareResponseObject(PeopleProcesses.GetAllStaffMembers(_context));
        }

        [HttpPost]
        [Route("get/dataGrid/all", Name = "ApiPeopleGetAllStaffMembersDataGrid")]
        [RequiresPermission("ViewStaff")]
        public IHttpActionResult GetAllStaffMembersDataGrid([FromBody] DataManagerRequest dm)
        {
            var staffMembers = PrepareResponseObject(PeopleProcesses.GetAllStaffMembers_DataGrid(_context));

            return PrepareDataGridObject(staffMembers, dm);
        }

        [Route("get/byId/{staffMemberId:int}", Name = "ApiPeopleGetStaffMemberById")]
        [RequiresPermission("ViewStaff")]
        public StaffMemberDto GetStaffMemberById([FromUri] int staffMemberId)
        {
            return PrepareResponseObject(PeopleProcesses.GetStaffMemberById(staffMemberId, _context));
        }

        [HttpGet]
        [RequiresPermission("EditStaff")]
        [Route("hasDocuments/{staffMemberId:int}", Name = "ApiPeopleStaffMemberHasDocuments")]
        public bool StaffMemberHasDocuments([FromUri] int staffMemberId)
        {
            return PrepareResponseObject(PeopleProcesses.StaffMemberHasDocuments(staffMemberId, _context));
        }

        [HttpGet]
        [RequiresPermission("EditStaff")]
        [Route("hasLogs/{staffMemberId:int}", Name = "ApiPeopleStaffHasLogs")]
        public bool StaffHasLogs([FromUri] int staffMemberId)
        {
            return PrepareResponseObject(PeopleProcesses.StaffMemberHasWrittenLogs(staffMemberId, _context));
        }
    }
}