using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    public class BehaviourController : MyPortalApiController
    {
        [HttpGet]
        [Route("api/behaviour/points/get/{studentId}")]
        public int GetBehaviourPoints(int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(
                BehaviourProcesses.GetBehaviourPointsCount(studentId, academicYearId, _context));
        }

        [HttpPost]
        [Route("api/behaviour/achievements/dataGrid/get/{studentId}")]
        public IHttpActionResult GetAchievementsForDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            var achievements =
                PrepareResponseObject(BehaviourProcesses.GetAchievementsForGrid(studentId, academicYearId, _context));

            return PrepareDataGridObject(achievements, dm);
        }

        [HttpGet]
        [Route("api/behaviour/achievements/get/{id}")]
        public BehaviourAchievementDto GetAchievement(int id)
        {
            return PrepareResponseObject(BehaviourProcesses.GetAchievement(id, _context));
        }

        [HttpPost]
        [Route("api/behaviour/achievements/create")]
        public IHttpActionResult CreateAchievement(BehaviourAchievement achievement)
        {
            var userId = User.Identity.GetUserId();
            var staff = PrepareResponseObject(PeopleProcesses.GetStaffFromUserId(userId, _context));

            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            achievement.AcademicYearId = academicYearId;
            achievement.RecordedById = staff.Id;

            return PrepareResponse(BehaviourProcesses.CreateAchievement(achievement, _context));
        }

        [HttpPost]
        [Route("api/behaviour/achievements/update")]
        public IHttpActionResult UpdateAchievement(BehaviourAchievement achievement)
        {
            return PrepareResponse(BehaviourProcesses.UpdateAchievement(achievement, _context));
        }

        [HttpDelete]
        [Route("api/behaviour/achievements/delete/{id}")]
        public IHttpActionResult DeleteAchievement(int id)
        {
            return PrepareResponse(BehaviourProcesses.DeleteAchievement(id, _context));
        }

        [HttpPost]
        [Route("api/behaviour/behaviour/dataGrid/get/{studentId}")]
        public IHttpActionResult GetBehaviourForDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var incidents =
                PrepareResponseObject(BehaviourProcesses.GetBehaviourIncidentsForGrid(studentId, academicYearId, _context));

            return PrepareDataGridObject(incidents, dm);
        }

        [HttpGet]
        [Route("api/behaviour/behaviour/get/{id}")]
        public BehaviourIncidentDto GetBehaviour(int id)
        {
            return PrepareResponseObject(BehaviourProcesses.GetBehaviourIncident(id, _context));
        }

        [HttpPost]
        [Route("api/behaviour/behaviour/create")]
        public IHttpActionResult CreateIncident(BehaviourIncident incident)
        {
            var userId = User.Identity.GetUserId();
            var staff = PrepareResponseObject(PeopleProcesses.GetStaffFromUserId(userId, _context));
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            incident.AcademicYearId = academicYearId;
            incident.RecordedById = staff.Id;

            return PrepareResponse(BehaviourProcesses.CreateBehaviourIncident(incident, _context));
        }

        [HttpPost]
        [Route("api/behaviour/behaviour/update")]
        public IHttpActionResult UpdateIncident(BehaviourIncident incident)
        {
            return PrepareResponse(BehaviourProcesses.UpdateBehaviourIncident(incident, _context));
        }

        [HttpDelete]
        [Route("api/behaviour/behaviour/delete/{id}")]
        public IHttpActionResult DeleteIncident(int id)
        {
            return PrepareResponse(BehaviourProcesses.DeleteBehaviourIncident(id, _context));
        }

        [HttpGet]
        [Route("api/behaviour/reports/incidents/byType")]
        public IEnumerable<ChartData> BehaviourIncidentsByType()
        {
            return PrepareResponseObject(BehaviourProcesses.GetChartData_BehaviourIncidentsByType(_context));
        }
    }
}
