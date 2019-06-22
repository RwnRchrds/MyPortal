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

            return BehaviourProcesses.GetBehaviourPoints(studentId, academicYearId, _context);
        }

        [HttpPost]
        [Route("api/behaviour/achievements/dataGrid/get/{studentId}")]
        public IHttpActionResult GetAchievementsForDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var achievements =
                _context.BehaviourAchievements.Where(
                        x => x.AcademicYearId == academicYearId && x.StudentId == studentId && !x.Deleted).OrderByDescending(x => x.Date).ToList()
                    .Select(Mapper.Map<BehaviourAchievement, GridAchievementDto>);

            var result = achievements.PerformDataOperations(dm);

            if (!dm.RequiresCounts) return Json(result);

            return Json(new {result = result.Items, count = result.Count});
        }

        [HttpGet]
        [Route("api/behaviour/achievements/get/{id}")]
        public BehaviourAchievementDto GetAchievement(int id)
        {
            var achievement = _context.BehaviourAchievements.SingleOrDefault(x => x.Id == id);

            if (achievement == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<BehaviourAchievement, BehaviourAchievementDto>(achievement);
        }

        [HttpPost]
        [Route("api/behaviour/achievements/create")]
        public IHttpActionResult CreateAchievement(BehaviourAchievement achievement)
        {
            var userId = User.Identity.GetUserId();
            var staff = PeopleProcesses.GetStaffFromUserId(userId, _context);

            if (staff == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            achievement.AcademicYearId = academicYearId;
            achievement.Date = DateTime.Today;
            achievement.RecordedById = staff.Id;
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            if (!achievement.Date.IsInAcademicYear(_context, academicYearId))
            {
                return Content(HttpStatusCode.BadRequest, "Specified date is not within the selected academic year");
            }

            _context.BehaviourAchievements.Add(achievement);
            _context.SaveChanges();

            return Ok("Achievement added");
        }

        [HttpPost]
        [Route("api/behaviour/achievements/update")]
        public IHttpActionResult UpdateAchievement(BehaviourAchievement achievement)
        {
            var achievementInDb = _context.BehaviourAchievements.SingleOrDefault(x => x.Id == achievement.Id);

            if (achievementInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Achievement not found");
            }

            achievementInDb.LocationId = achievement.LocationId;
            achievementInDb.Comments = achievement.Comments;
            achievementInDb.Points = achievement.Points;
            achievementInDb.Resolved = achievement.Resolved;
            achievementInDb.AchievementTypeId = achievement.AchievementTypeId;

            _context.SaveChanges();

            return Ok("Achievement updated");
        }

        [HttpDelete]
        [Route("api/behaviour/achievements/delete/{id}")]
        public IHttpActionResult DeleteAchievement(int id)
        {
            var achievement = _context.BehaviourAchievements.SingleOrDefault(x => x.Id == id);

            if (achievement == null)
            {
                return Content(HttpStatusCode.NotFound, "Achievement not found");
            }

            achievement.Deleted = true;

            //_context.BehaviourAchievements.Remove(achievement);
            _context.SaveChanges();

            return Ok("Achievement deleted");
        }

        [HttpPost]
        [Route("api/behaviour/behaviour/dataGrid/get/{studentId}")]
        public IHttpActionResult GetBehaviourForDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var incidents =
                _context.BehaviourIncidents.Where(
                        x => x.AcademicYearId == academicYearId && x.StudentId == studentId && !x.Deleted)
                    .OrderByDescending(x => x.Date).ToList().Select(Mapper.Map<BehaviourIncident, GridIncidentDto>);

            var result = incidents.PerformDataOperations(dm);

            if (!dm.RequiresCounts) return Json(result);

            return Json(new {result = result.Items, count = result.Count});
        }

        [HttpGet]
        [Route("api/behaviour/behaviour/get/{id}")]
        public BehaviourIncidentDto GetBehaviour(int id)
        {
            var incident = _context.BehaviourIncidents.SingleOrDefault(x => x.Id == id);

            if (incident == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<BehaviourIncident, BehaviourIncidentDto>(incident);
        }

        [HttpPost]
        [Route("api/behaviour/behaviour/create")]
        public IHttpActionResult CreateIncident(BehaviourIncident incident)
        {
            var userId = User.Identity.GetUserId();
            var staff = PeopleProcesses.GetStaffFromUserId(userId, _context);

            if (staff == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            incident.AcademicYearId = academicYearId;
            incident.Date = DateTime.Today;
            incident.RecordedById = staff.Id;

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            if (!incident.Date.IsInAcademicYear(_context, academicYearId))
            {
                return Content(HttpStatusCode.BadRequest, "Specified date is not within the selected academic year");
            }

            _context.BehaviourIncidents.Add(incident);
            _context.SaveChanges();

            return Ok("Incident added");
        }

        [HttpPost]
        [Route("api/behaviour/behaviour/update")]
        public IHttpActionResult UpdateIncident(BehaviourIncident incident)
        {
            var incidentInDb = _context.BehaviourIncidents.SingleOrDefault(x => x.Id == incident.Id);

            if (incidentInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Achievement not found");
            }

            incidentInDb.LocationId = incident.LocationId;
            incidentInDb.Comments = incident.Comments;
            incidentInDb.Points = incident.Points;
            incidentInDb.Resolved = incident.Resolved;
            incidentInDb.BehaviourTypeId = incident.BehaviourTypeId;

            _context.SaveChanges();

            return Ok("Incident updated");
        }

        [HttpDelete]
        [Route("api/behaviour/behaviour/delete/{id}")]
        public IHttpActionResult DeleteIncident(int id)
        {
            var incident = _context.BehaviourIncidents.SingleOrDefault(x => x.Id == id);

            if (incident == null)
            {
                return Content(HttpStatusCode.NotFound, "Achievement not found");
            }

            incident.Deleted = true;

            //_context.BehaviourIncidents.Remove(incident);
            _context.SaveChanges();

            return Ok("Incident deleted");
        }
    }
}
