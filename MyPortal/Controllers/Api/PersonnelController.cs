using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/personnel")]
    [Authorize]
    public class PersonnelController : MyPortalApiController
    {
        private readonly PersonnelService _service;

        public PersonnelController()
        {
            _service = new PersonnelService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
        
        [HttpPost]
        [RequiresPermission("EditTrainingCertificates")]
        [Route("certificates/create", Name = "ApiCreateTrainingCertificate")]
        public async Task<IHttpActionResult> CreateTrainingCertificate([FromBody] TrainingCertificateDto certificate)
        {
            try
            {
                await _service.CreateTrainingCertificate(certificate);
                await _service.SaveChanges();

                return Ok("Certificate created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditTrainingCertificates")]
        [Route("certificates/delete/{staffId:int}/{courseId:int}", Name = "ApiDeleteTrainingCertificate")]
        public async Task<IHttpActionResult> DeleteCertificate([FromUri] int staffId, [FromUri] int courseId)
        {
            try
            {
                await _service.DeleteTrainingCertificate(staffId, courseId);
                await _service.SaveChanges();

                return Ok("Certificate deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        
        [HttpGet]
        [RequiresPermission("ViewTrainingCertificates")]
        [Route("certificates/get/{staffId:int}/{courseId:int}", Name = "ApiGetTrainingCertificate")]
        public async Task<TrainingCertificateDto> GetTrainingCertificate([FromUri] int staffId, [FromUri] int courseId)
        {
            try
            {
                return await _service.GetCertificate(staffId, courseId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewTrainingCertificates")]
        [Route("certificates/get/byStaff/{staffId:int}", Name = "ApiGetTrainingCertificatesByStaffMember")]
        public async Task<IEnumerable<TrainingCertificateDto>> GetCertificatesByStaffMember([FromUri] int staffId)
        {
            try
            {
                return await _service.GetCertificatesByStaffMember(staffId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewTrainingCertificates")]
        [Route("certificates/get/byStaff/dataGrid/{staffId:int}", Name = "ApiGetTrainingCertificatesByStaffMemberDataGrid")]
        public async Task<IHttpActionResult> GetCertificatesForStaffMemberDataGrid([FromUri] int staffId, [FromBody] DataManagerRequest dm)
        {
            try
            {
                var certificates = await _service.GetCertificatesByStaffMember(staffId);

                var list = certificates.Select(_mapper.Map<DataGridTrainingCertificateDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditTrainingCertificates")]
        [Route("certificates/update", Name = "ApiUpdateTrainingCertificate")]
        public async Task<IHttpActionResult> UpdateCertificate([FromBody] TrainingCertificateDto certificate)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _service.UpdateCertificate(certificate);
                await _service.SaveChanges();

                return Ok("Certificate updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditTrainingCourses")]
        [Route("courses/remove/{courseId:int}", Name = "ApiDeleteTrainingCourse")]
        public async Task<IHttpActionResult> DeleteCourse([FromUri] int courseId)
        {
            try
            {
                await _service.DeleteCourse(courseId);
                await _service.SaveChanges();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Course deleted");
        }

        [HttpGet]
        [Route("courses/get/byId/{courseId:int}", Name = "ApiGetTrainingCourseById")]
        [RequiresPermission("ViewTrainingCourses")]
        public async Task<TrainingCourseDto> GetCourseById([FromUri] int courseId)
        {
            try
            {
                return await _service.GetCourseById(courseId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [Route("courses/get/all", Name = "ApiGetAllTrainingCourses")]
        [RequiresPermission("ViewTrainingCourses")]
        public async Task<IEnumerable<TrainingCourseDto>> GetCourses()
        {
            try
            {
                return await _service.GetAllTrainingCourses();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [Route("courses/get/dataGrid/all", Name = "ApiGetAllTrainingCoursesDataGrid")]
        [RequiresPermission("ViewTrainingCourses")]
        public async Task<IHttpActionResult> GetAllTrainingCourseDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var courses = await _service.GetAllTrainingCourses();

                var list = courses.Select(_mapper.Map<DataGridTrainingCourseDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("courses/edit", Name = "ApiUpdateTrainingCourse")]
        [RequiresPermission("EditTrainingCourses")]
        public async Task<IHttpActionResult> UpdateCourse([FromBody] TrainingCourseDto course)
        {
            try
            {
                await _service.UpdateCourse(course);
                await _service.SaveChanges();

                return Ok("Training course updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("observations/create", Name = "ApiCreateObservation")]
        [RequiresPermission("EditObservations")]
        public async Task<IHttpActionResult> CreateObservation([FromBody] ObservationDto data)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _service.CreateObservation(data, userId);
                await _service.SaveChanges();

                return Ok("Observation created.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewObservations")]
        [Route("observations/get/byId/{observationId:int}", Name = "ApiGetObservationById")]
        public async Task<ObservationDto> GetObservation([FromUri] int observationId)
        {
            try
            {
                return await _service.GetObservationById(observationId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }  
        
        [HttpGet]
        [RequiresPermission("ViewObservations")]
        [Route("observations/get/byStaff/{staffMemberId:int}", Name = "ApiGetObservationsByStaffMember")]
        public async Task<IEnumerable<ObservationDto>> GetObservationsByStaffMember([FromUri] int staffMemberId)
        {
            try
            {
                var observations = await _service.GetObservationsByStaffMember(staffMemberId);

                return observations.Select(_mapper.Map<ObservationDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewObservations")]
        [Route("observations/get/byStaff/dataGrid/{staffId:int}", Name = "ApiGetObservationsByStaffMemberDataGrid")]
        public async Task<IHttpActionResult> GetObservationsForStaffMemberDataGrid([FromUri] int staffId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var observations = await _service.GetObservationsByStaffMember(staffId);

                var list = observations.Select(_mapper.Map<DataGridObservationDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditObservations")]
        [Route("observations/delete/{observationId:int}", Name = "ApiDeleteObservation")]
        public async Task<IHttpActionResult> RemoveObservation([FromUri] int observationId)
        {
            try
            {
                await _service.DeleteObservation(observationId);
                await _service.SaveChanges();

                return Ok("Observation deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditObservations")]
        [Route("observations/update", Name = "ApiUpdateObservation")]
        public async Task<IHttpActionResult> UpdateObservation([FromBody] ObservationDto observation)
        {
            try
            {
                await _service.UpdateObservation(observation);
                await _service.SaveChanges();

                return Ok("Observation updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
