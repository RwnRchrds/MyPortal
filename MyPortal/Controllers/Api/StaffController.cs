using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Processes;

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

        [Route("get/all")]
        public IEnumerable<StaffMemberDto> GetAllStaffMembers()
        {
            return PrepareResponseObject(PeopleProcesses.GetAllStaffMembers(_context));
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