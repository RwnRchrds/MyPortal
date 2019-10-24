using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Models.Database;
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
        public async Task<int> GetBehaviourPointsByStudent([FromUri] int studentId)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            try
            {
                return await BehaviourProcesses.GetBehaviourPointsCountByStudent(studentId, academicYearId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewBehaviour")]
        [Route("achievements/get/byStudent/dataGrid/{studentId:int}", Name = "ApiBehaviourGetAchievementsByStudentDataGrid")]
        public async Task<IHttpActionResult> GetAchievementsByStudentDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            try
            {
                var achievements = await BehaviourProcesses.GetAchievementsForGrid(studentId, academicYearId, _context);
                return PrepareDataGridObject(achievements, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewBehaviour")]
        [Route("achievements/get/byId/{achievementId:int}", Name = "ApiBehaviourGetAchievementById")]
        public async Task<BehaviourAchievementDto> GetAchievementById([FromUri] int achievementId)
        {
            try
            {
                return await BehaviourProcesses.GetAchievementById(achievementId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("achievements/create", Name = "ApiBehaviourCreateAchievement")]
        public async Task<IHttpActionResult> CreateAchievement([FromBody] BehaviourAchievement achievement)
        {
            var userId = User.Identity.GetUserId();
            var staff = PrepareResponseObject(PeopleProcesses.GetStaffFromUserId(userId, _context));

            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            achievement.AcademicYearId = academicYearId;
            achievement.RecordedById = staff.Id;

            try
            {
                await BehaviourProcesses.CreateAchievement(achievement, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Achievement created");
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("achievements/update", Name = "ApiBehaviourUpdateAchievement")]
        public async Task<IHttpActionResult> UpdateAchievement([FromBody] BehaviourAchievement achievement)
        {
            try
            {
                await BehaviourProcesses.UpdateAchievement(achievement, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Achievement updated");
        }

        [HttpDelete]
        [RequiresPermission("EditBehaviour")]
        [Route("achievements/delete/{achievementId:int}", Name = "ApiBehaviourDeleteAchievement")]
        public async Task<IHttpActionResult> DeleteAchievement([FromUri] int achievementId)
        {
            try
            {
                await BehaviourProcesses.DeleteAchievement(achievementId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Achievement deleted");
        }

        [HttpPost]
        [RequiresPermission("ViewBehaviour")]
        [Route("incidents/get/byStudent/dataGrid/{studentId:int}", Name = "ApiBehaviourGetBehaviourIncidentsByStudentDataGrid")]
        public async Task<IHttpActionResult> GetBehaviourIncidentsByStudentDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            try
            {
                var incidents = await BehaviourProcesses.GetBehaviourIncidentsForGrid(studentId, academicYearId, _context);
                return PrepareDataGridObject(incidents, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewBehaviour")]
        [Route("incidents/get/byId/{incidentId:int}", Name = "ApiBehaviourGetBehaviourIncidentById")]
        public async  Task<BehaviourIncidentDto> GetBehaviourIncidentById([FromUri] int incidentId)
        {
            try
            {
                return await BehaviourProcesses.GetBehaviourIncidentById(incidentId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("incidents/create", Name = "ApiBehaviourCreateIncident")]
        public async Task<IHttpActionResult> CreateIncident([FromBody] BehaviourIncident incident)
        {
            var userId = User.Identity.GetUserId();
            var staff = PrepareResponseObject(PeopleProcesses.GetStaffFromUserId(userId, _context));
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            incident.AcademicYearId = academicYearId;
            incident.RecordedById = staff.Id;

            try
            {
                await BehaviourProcesses.CreateBehaviourIncident(incident, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Incident created");
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("incidents/update", Name = "ApiBehaviourUpdateIncident")]
        public async Task<IHttpActionResult> UpdateIncident([FromBody] BehaviourIncident incident)
        {
            try
            {
                await BehaviourProcesses.UpdateBehaviourIncident(incident, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Incident updated");
        }

        [HttpDelete]
        [RequiresPermission("EditBehaviour")]
        [Route("incidents/delete/{incidentId:int}", Name = "ApiBehaviourDeleteIncident")]
        public async Task<IHttpActionResult> DeleteIncident([FromUri] int incidentId)
        {
            try
            {
                await BehaviourProcesses.DeleteBehaviourIncident(incidentId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Incident deleted");
        }
    }
}
