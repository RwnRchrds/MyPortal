using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
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
        [Route("create")]
        public IHttpActionResult CreateStaff([FromBody] StaffMember staffMember)
        {
            return PrepareResponse(PeopleProcesses.CreateStaffMember(staffMember, _context));
        }

        [HttpDelete]
        [Route("delete/{staffMemberId:int}")]
        public IHttpActionResult DeleteStaff([FromUri] int staffMemberId)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PeopleProcesses.DeleteStaffMember(staffMemberId, userId, _context));
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateStaffMember([FromBody] StaffMember staffMember)
        {
            return PrepareResponse(PeopleProcesses.UpdateStaffMember(staffMember, _context));
        }

        [HttpGet]
        [Route("get/all")]
        public IEnumerable<StaffMemberDto> GetAllStaffMembers()
        {
            return PrepareResponseObject(PeopleProcesses.GetAllStaffMembers(_context));
        }

        [HttpPost]
        [Route("get/dataGrid/all")]
        public IHttpActionResult GetAllStaffMembersForDataGrid([FromBody] DataManagerRequest dm)
        {
            var staffMembers = PrepareResponseObject(PeopleProcesses.GetAllStaffMembers_DataGrid(_context));

            return PrepareDataGridObject(staffMembers, dm);
        }

        [Route("get/byId/{staffMemberId:int}")]
        public StaffMemberDto GetStaffMemberById([FromUri] int staffMemberId)
        {
            return PrepareResponseObject(PeopleProcesses.GetStaffMemberById(staffMemberId, _context));
        }

        [HttpGet]
        [Route("hasDocuments/{staffMemberId:int}")]
        public bool StaffMemberHasDocuments([FromUri] int staffMemberId)
        {
            return PrepareResponseObject(PeopleProcesses.StaffMemberHasDocuments(staffMemberId, _context));
        }

        [HttpGet]
        [Route("hasLogs/{staffMemberId:int}")]
        public bool StaffHasLogs([FromUri] int staffMemberId)
        {
            return PrepareResponseObject(PeopleProcesses.StaffMemberHasWrittenLogs(staffMemberId, _context));
        }
    }
}