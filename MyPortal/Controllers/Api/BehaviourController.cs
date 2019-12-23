using System;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Services;
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
            _service = new BehaviourService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
        
        [HttpGet]
        [RequiresPermission("ViewBehaviour")]
        [Route("points/get/{studentId:int}", Name = "ApiGetBehaviourPointsByStudent")]
        public async Task<int> GetBehaviourPointsByStudent([FromUri] int studentId)
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                return await _service.GetBehaviourPointsCountByStudent(studentId, academicYearId);
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
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();
                var achievements = await _service.GetAchievementsByStudent(studentId, academicYearId);

                var list = achievements.Select(_mapping.Map<DataGridAchievementDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewBehaviour")]
        [Route("achievements/get/byId/{achievementId:int}", Name = "ApiGetAchievementById")]
        public async Task<AchievementDto> GetAchievementById([FromUri] int achievementId)
        {
            try
            {
                var achievement = await _service.GetAchievementById(achievementId);

                return achievement;
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("achievements/create", Name = "ApiCreateAchievement")]
        public async Task<IHttpActionResult> CreateAchievement([FromBody] AchievementDto achievement)
        {
            try
            {
                using(var staffService = new StaffMemberService())
                {
                    var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();
                    var userId = User.Identity.GetUserId();

                    var recordedBy = staffService.GetStaffMemberByUserId(userId);

                    achievement.RecordedById = recordedBy.Id;
                    achievement.AcademicYearId = academicYearId;

                    await _service.CreateAchievement(achievement);

                    return Ok("Achievement created");
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("achievements/update", Name = "ApiUpdateAchievement")]
        public async Task<IHttpActionResult> UpdateAchievement([FromBody] AchievementDto achievement)
        {
            try
            {
                await _service.UpdateAchievement(achievement);

                return Ok("Achievement updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditBehaviour")]
        [Route("achievements/delete/{achievementId:int}", Name = "ApiDeleteAchievement")]
        public async Task<IHttpActionResult> DeleteAchievement([FromUri] int achievementId)
        {
            try
            {
                await _service.DeleteAchievement(achievementId);

                return Ok("Achievement deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewBehaviour")]
        [Route("incidents/get/byStudent/dataGrid/{studentId:int}", Name = "ApiGetBehaviourIncidentsByStudentDataGrid")]
        public async Task<IHttpActionResult> GetBehaviourIncidentsByStudentDataGrid([FromBody] DataManagerRequest dm, [FromUri] int studentId)
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var incidents = await _service.GetBehaviourIncidentsByStudent(studentId, academicYearId);

                var list = incidents.Select(_mapping.Map<DataGridIncidentDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewBehaviour")]
        [Route("incidents/get/byId/{incidentId:int}", Name = "ApiGetBehaviourIncidentById")]
        public async  Task<IncidentDto> GetBehaviourIncidentById([FromUri] int incidentId)
        {
            try
            {
                return await _service.GetBehaviourIncidentById(incidentId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("incidents/create", Name = "ApiCreateIncident")]
        public async Task<IHttpActionResult> CreateIncident([FromBody] IncidentDto incident)
        {
            try
            {
                using (var staffService = new StaffMemberService())
                {
                    var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();
                    var userId = User.Identity.GetUserId();
                    var recordedBy = await staffService.GetStaffMemberByUserId(userId);

                    incident.RecordedById = recordedBy.Id;
                    incident.AcademicYearId = academicYearId;

                    await _service.CreateBehaviourIncident(incident);

                    return Ok("Incident created");
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditBehaviour")]
        [Route("incidents/update", Name = "ApiUpdateIncident")]
        public async Task<IHttpActionResult> UpdateIncident([FromBody] IncidentDto incident)
        {
            try
            {
                await _service.UpdateBehaviourIncident(incident);

                return Ok("Incident updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditBehaviour")]
        [Route("incidents/delete/{incidentId:int}", Name = "ApiDeleteIncident")]
        public async Task<IHttpActionResult> DeleteIncident([FromUri] int incidentId)
        {
            try
            {
                await _service.DeleteBehaviourIncident(incidentId);

                return Ok("Incident deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
