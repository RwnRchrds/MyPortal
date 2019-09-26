using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models.Attributes;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/behaviour")]
    public class BehaviourController : MyPortalApiController
    {
        [HttpGet]
        [RequiresPermission("ViewBehaviour")]
        [Route("points/get/{studentId:int}", Name = "ApiBehaviourGetBehaviourPointsByStudent")]
        public int GetBehaviourPointsByStudent([FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(
                BehaviourProcesses.GetBehaviourPointsCountByStudent(studentId, academicYearId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewBehaviour")]
        [Route("achievements/get/byStudent/dataGrid/{studentId:int}", Name = "ApiBehaviourGetAchievementsByStudentDataGrid")]
        public IHttpActionResult GetAchievementsByStudentDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            var achievements =
                PrepareResponseObject(BehaviourProcesses.GetAchievementsForGrid(studentId, academicYearId, _context));

            return PrepareDataGridObject(achievements, dm);
        }

        [HttpGet]
        [RequiresPermission("ViewBehaviour")]
        [Route("achievements/get/byId/{achievementId:int}", Name = "ApiBehaviourGetAchievementById")]
        public BehaviourAchievementDto GetAchievementById([FromUri] int achievementId)
        {
            return PrepareResponseObject(BehaviourProcesses.GetAchievementById(achievementId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("achievements/create", Name = "ApiBehaviourCreateAchievement")]
        public IHttpActionResult CreateAchievement([FromBody] BehaviourAchievement achievement)
        {
            var userId = User.Identity.GetUserId();
            var staff = PrepareResponseObject(PeopleProcesses.GetStaffFromUserId(userId, _context));

            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            achievement.AcademicYearId = academicYearId;
            achievement.RecordedById = staff.Id;

            return PrepareResponse(BehaviourProcesses.CreateAchievement(achievement, _context));
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("achievements/update", Name = "ApiBehaviourUpdateAchievement")]
        public IHttpActionResult UpdateAchievement([FromBody] BehaviourAchievement achievement)
        {
            return PrepareResponse(BehaviourProcesses.UpdateAchievement(achievement, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditBehaviour")]
        [Route("achievements/delete/{achievementId:int}", Name = "ApiBehaviourDeleteAchievement")]
        public IHttpActionResult DeleteAchievement([FromUri] int achievementId)
        {
            return PrepareResponse(BehaviourProcesses.DeleteAchievement(achievementId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewBehaviour")]
        [Route("incidents/get/byStudent/dataGrid/{studentId:int}", Name = "ApiBehaviourGetBehaviourIncidentsByStudentDataGrid")]
        public IHttpActionResult GetBehaviourIncidentsByStudentDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var incidents =
                PrepareResponseObject(BehaviourProcesses.GetBehaviourIncidentsForGrid(studentId, academicYearId, _context));

            return PrepareDataGridObject(incidents, dm);
        }

        [HttpGet]
        [RequiresPermission("ViewBehaviour")]
        [Route("incidents/get/byId/{incidentId:int}", Name = "ApiBehaviourGetBehaviourIncidentById")]
        public BehaviourIncidentDto GetBehaviourIncidentById([FromUri] int incidentId)
        {
            return PrepareResponseObject(BehaviourProcesses.GetBehaviourIncident(incidentId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("incidents/create", Name = "ApiBehaviourCreateIncident")]
        public IHttpActionResult CreateIncident([FromBody] BehaviourIncident incident)
        {
            var userId = User.Identity.GetUserId();
            var staff = PrepareResponseObject(PeopleProcesses.GetStaffFromUserId(userId, _context));
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            incident.AcademicYearId = academicYearId;
            incident.RecordedById = staff.Id;

            return PrepareResponse(BehaviourProcesses.CreateBehaviourIncident(incident, _context));
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("incidents/update", Name = "ApiBehaviourUpdateIncident")]
        public IHttpActionResult UpdateIncident([FromBody] BehaviourIncident incident)
        {
            return PrepareResponse(BehaviourProcesses.UpdateBehaviourIncident(incident, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditBehaviour")]
        [Route("incidents/delete/{incidentId:int}", Name = "ApiBehaviourDeleteIncident")]
        public IHttpActionResult DeleteIncident([FromUri] int incidentId)
        {
            return PrepareResponse(BehaviourProcesses.DeleteBehaviourIncident(incidentId, _context));
        }
    }
}
