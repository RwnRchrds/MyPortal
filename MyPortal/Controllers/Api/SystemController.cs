using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models;
using MyPortal.Models.Attributes;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/system")]
    public class SystemController : MyPortalApiController
    {
        [HttpPost]
        [HasPermission("EditBulletins")]
        [Route("bulletins/create")]
        public IHttpActionResult CreateBulletin([FromBody] SystemBulletin bulletin)
        {
            var userId = User.Identity.GetUserId();

            var autoApprove = User.IsInRole("SeniorStaff");

            return PrepareResponse(SystemProcesses.CreateBulletin(bulletin, userId, _context, autoApprove));
        }

        [HttpPost]
        [HasPermission("EditBulletins")]
        public IHttpActionResult UpdateBulletin([FromBody] SystemBulletin bulletin)
        {
            var approvable = User.IsInRole("SeniorStaff");

            return PrepareResponse(SystemProcesses.UpdateBulletin(bulletin, _context, approvable));
        }

        [HttpDelete]
        [HasPermission("EditBulletins")]
        [Route("bulletins/delete/{bulletinId:int}")]
        public IHttpActionResult DeleteBulletin([FromUri] int bulletinId)
        {
            return PrepareResponse(SystemProcesses.DeleteBulletin(bulletinId, _context));
        }

        [HttpGet]
        [HasPermission("ViewAllBulletins")]
        [Route("bulletins/get/all")]
        public IEnumerable<SystemBulletinDto> GetAllBulletins()
        {
            return PrepareResponseObject(SystemProcesses.GetAllBulletins(_context));
        }

        [HttpGet]
        [HasPermission("ViewStaffBulletins")]
        [Route("bulletins/get/approved")]
        public IEnumerable<SystemBulletinDto> GetApprovedBulletins()
        {
            return PrepareResponseObject(SystemProcesses.GetApprovedBulletins(_context));
        }

        [HttpGet]
        [HasPermission("ViewStaffBulletins")]
        [Route("bulletins/get/own")]
        public IEnumerable<SystemBulletinDto> GetOwnBulletins()
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponseObject(SystemProcesses.GetOwnBulletins(userId, _context));
        }

        [HttpGet]
        [HasPermission("ViewStudentBulletins")]
        [Route("bulletins/get/student")]
        public IEnumerable<SystemBulletinDto> GetStudentBulletins()
        {
            return PrepareResponseObject(SystemProcesses.GetApprovedStudentBulletins(_context));
        }
    }
}