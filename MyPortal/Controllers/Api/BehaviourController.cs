using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
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
        [Route("points/get/{studentId:int}")]
        public int GetBehaviourPoints([FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(
                BehaviourProcesses.GetBehaviourPointsCount(studentId, academicYearId, _context));
        }

        [HttpPost]
        [Route("achievements/get/byStudent/dataGrid/{studentId:int}")]
        public IHttpActionResult GetAchievementsForDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            var achievements =
                PrepareResponseObject(BehaviourProcesses.GetAchievementsForGrid(studentId, academicYearId, _context));

            return PrepareDataGridObject(achievements, dm);
        }

        [HttpGet]
        [Route("achievements/get/byId/{achievementId:int}")]
        public BehaviourAchievementDto GetAchievement([FromUri] int achievementId)
        {
            return PrepareResponseObject(BehaviourProcesses.GetAchievement(achievementId, _context));
        }

        [HttpPost]
        [Route("achievements/create")]
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
        [Route("achievements/update")]
        public IHttpActionResult UpdateAchievement([FromBody] BehaviourAchievement achievement)
        {
            return PrepareResponse(BehaviourProcesses.UpdateAchievement(achievement, _context));
        }

        [HttpDelete]
        [Route("achievements/delete/{achievementId:int}")]
        public IHttpActionResult DeleteAchievement([FromUri] int achievementId)
        {
            return PrepareResponse(BehaviourProcesses.DeleteAchievement(achievementId, _context));
        }

        [HttpPost]
        [Route("incidents/get/byStudent/dataGrid/{studentId:int}")]
        public IHttpActionResult GetBehaviourForDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var incidents =
                PrepareResponseObject(BehaviourProcesses.GetBehaviourIncidentsForGrid(studentId, academicYearId, _context));

            return PrepareDataGridObject(incidents, dm);
        }

        [HttpGet]
        [Route("incidents/get/byId/{incidentId:int}")]
        public BehaviourIncidentDto GetBehaviour([FromUri] int incidentId)
        {
            return PrepareResponseObject(BehaviourProcesses.GetBehaviourIncident(incidentId, _context));
        }

        [HttpPost]
        [Route("incidents/create")]
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
        [Route("incidents/update")]
        public IHttpActionResult UpdateIncident([FromBody] BehaviourIncident incident)
        {
            return PrepareResponse(BehaviourProcesses.UpdateBehaviourIncident(incident, _context));
        }

        [HttpDelete]
        [Route("incidents/delete/{incidentId:int}")]
        public IHttpActionResult DeleteIncident([FromUri] int incidentId)
        {
            return PrepareResponse(BehaviourProcesses.DeleteBehaviourIncident(incidentId, _context));
        }

        [HttpGet]
        [Route("reports/incidents/byType")]
        public IEnumerable<ChartDataCategoric> ReportIncidentsByType()
        {
            return PrepareResponseObject(BehaviourProcesses.GetChartData_BehaviourIncidentsByType(_context));
        }
    }
}
