using System;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/behaviour")]
    public class BehaviourController : MyPortalApiController
    {
        private readonly BehaviourService _service;

        public BehaviourController()
        {
            _service = new BehaviourService(UnitOfWork);
        }
        
        [HttpGet]
        [RequiresPermission("ViewBehaviour")]
        [Route("points/get/{studentId:int}", Name = "ApiGetBehaviourPointsByStudent")]
        public async Task<int> GetBehaviourPointsByStudent([FromUri] int studentId)
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var points = await _service.GetBehaviourPointsCountByStudent(studentId, academicYearId);

                    return points;
                }
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewBehaviour")]
        [Route("achievements/get/byStudent/dataGrid/{studentId:int}", Name = "ApiGetAchievementsByStudentDataGrid")]
        public async Task<IHttpActionResult> GetAchievementsByStudentDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);
                    var achievements = await _service.GetAchievementsByStudent(studentId, academicYearId);

                    var list = achievements.Select(Mapper.Map<BehaviourAchievement, GridBehaviourAchievementDto>);
                    
                    return PrepareDataGridObject(list, dm);   
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewBehaviour")]
        [Route("achievements/get/byId/{achievementId:int}", Name = "ApiGetAchievementById")]
        public async Task<BehaviourAchievementDto> GetAchievementById([FromUri] int achievementId)
        {
            try
            {
                var achievement = await _service.GetAchievementById(achievementId);

                return Mapper.Map<BehaviourAchievement, BehaviourAchievementDto>(achievement);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("achievements/create", Name = "ApiCreateAchievement")]
        public async Task<IHttpActionResult> CreateAchievement([FromBody] BehaviourAchievement achievement)
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                using(var staffService = new StaffMemberService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);
                    var userId = User.Identity.GetUserId();

                    var recordedBy = staffService.GetStaffMemberFromUserId(userId);

                    achievement.RecordedById = recordedBy.Id;
                    achievement.AcademicYearId = academicYearId;

                    await _service.CreateAchievement(achievement);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Achievement created");
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("achievements/update", Name = "ApiUpdateAchievement")]
        public async Task<IHttpActionResult> UpdateAchievement([FromBody] BehaviourAchievement achievement)
        {
            try
            {
                await _service.UpdateAchievement(achievement);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Achievement updated");
        }

        [HttpDelete]
        [RequiresPermission("EditBehaviour")]
        [Route("achievements/delete/{achievementId:int}", Name = "ApiDeleteAchievement")]
        public async Task<IHttpActionResult> DeleteAchievement([FromUri] int achievementId)
        {
            try
            {
                await _service.DeleteAchievement(achievementId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Achievement deleted");
        }

        [HttpPost]
        [RequiresPermission("ViewBehaviour")]
        [Route("incidents/get/byStudent/dataGrid/{studentId:int}", Name = "ApiGetBehaviourIncidentsByStudentDataGrid")]
        public async Task<IHttpActionResult> GetBehaviourIncidentsByStudentDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var incidents = await _service.GetBehaviourIncidentsByStudent(studentId, academicYearId);

                    var list = incidents.Select(Mapper.Map<BehaviourIncident, GridBehaviourIncidentDto>);

                    return PrepareDataGridObject(list, dm);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewBehaviour")]
        [Route("incidents/get/byId/{incidentId:int}", Name = "ApiGetBehaviourIncidentById")]
        public async  Task<BehaviourIncidentDto> GetBehaviourIncidentById([FromUri] int incidentId)
        {
            try
            {
                var incident = await _service.GetBehaviourIncidentById(incidentId);

                return Mapper.Map<BehaviourIncident, BehaviourIncidentDto>(incident);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("incidents/create", Name = "ApiCreateIncident")]
        public async Task<IHttpActionResult> CreateIncident([FromBody] BehaviourIncident incident)
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                using (var staffService = new StaffMemberService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);
                    var userId = User.Identity.GetUserId();
                    var recordedBy = await staffService.GetStaffMemberFromUserId(userId);

                    incident.RecordedById = recordedBy.Id;
                    incident.AcademicYearId = academicYearId;

                    await _service.CreateBehaviourIncident(incident);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Incident created");
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("incidents/update", Name = "ApiUpdateIncident")]
        public async Task<IHttpActionResult> UpdateIncident([FromBody] BehaviourIncident incident)
        {
            try
            {
                await _service.UpdateBehaviourIncident(incident);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Incident updated");
        }

        [HttpDelete]
        [RequiresPermission("EditBehaviour")]
        [Route("incidents/delete/{incidentId:int}", Name = "ApiDeleteIncident")]
        public async Task<IHttpActionResult> DeleteIncident([FromUri] int incidentId)
        {
            try
            {
                await _service.DeleteBehaviourIncident(incidentId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Incident deleted");
        }
    }
}
