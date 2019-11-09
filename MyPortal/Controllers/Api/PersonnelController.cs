using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Dtos.DataGrid;
using MyPortal.Models.Database;
using MyPortal.Services;
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
            _service = new PersonnelService(UnitOfWork);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
        
        [HttpPost]
        [RequiresPermission("EditTrainingCertificates")]
        [Route("certificates/create", Name = "ApiCreateTrainingCertificate")]
        public async Task<IHttpActionResult> CreateTrainingCertificate([FromBody] PersonnelTrainingCertificate certificate)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                await _service.CreateTrainingCertificate(certificate, userId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Certificate created");
        }

        [HttpDelete]
        [RequiresPermission("EditTrainingCertificates")]
        [Route("certificates/delete/{staffId:int}/{courseId:int}", Name = "ApiDeleteTrainingCertificate")]
        public async Task<IHttpActionResult> DeleteCertificate([FromUri] int staffId, [FromUri] int courseId)
        {
            try
            {
                await _service.DeleteTrainingCertificate(staffId, courseId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Certificate deleted");
        }
        
        [HttpGet]
        [RequiresPermission("ViewTrainingCertificates")]
        [Route("certificates/get/{staffId:int}/{courseId:int}", Name = "ApiGetTrainingCertificate")]
        public async Task<PersonnelTrainingCertificateDto> GetTrainingCertificate([FromUri] int staffId, [FromUri] int courseId)
        {
            try
            {
                var certificate = await _service.GetCertificate(staffId, courseId);

                return Mapper.Map<PersonnelTrainingCertificate, PersonnelTrainingCertificateDto>(certificate);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewTrainingCertificates")]
        [Route("certificates/get/byStaff/{staffId:int}", Name = "ApiGetTrainingCertificatesByStaffMember")]
        public async Task<IEnumerable<PersonnelTrainingCertificateDto>> GetCertificatesByStaffMember([FromUri] int staffId)
        {
            try
            {
                var certificates = await _service.GetCertificatesByStaffMember(staffId);

                return certificates.Select(Mapper.Map<PersonnelTrainingCertificate, PersonnelTrainingCertificateDto>);
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

                var list = certificates.Select(Mapper.Map<PersonnelTrainingCertificate, GridPersonnelTrainingCertificateDto>);

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
        public async Task<IHttpActionResult> UpdateCertificate([FromBody] PersonnelTrainingCertificate certificate)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _service.UpdateCertificate(certificate, userId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Certificate updated");
        }

        [HttpDelete]
        [RequiresPermission("EditTrainingCourses")]
        [Route("courses/remove/{courseId:int}", Name = "ApiDeleteTrainingCourse")]
        public async Task<IHttpActionResult> DeleteCourse([FromUri] int courseId)
        {
            try
            {
                await _service.DeleteCourse(courseId);
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
        public async Task<PersonnelTrainingCourseDto> GetCourseById([FromUri] int courseId)
        {
            try
            {
                var course = await _service.GetCourseById(courseId);

                return Mapper.Map<PersonnelTrainingCourse, PersonnelTrainingCourseDto>(course);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [Route("courses/get/all", Name = "ApiGetAllTrainingCourses")]
        [RequiresPermission("ViewTrainingCourses")]
        public async Task<IEnumerable<PersonnelTrainingCourseDto>> GetCourses()
        {
            try
            {
                var courses = await _service.GetAllTrainingCourses();

                return courses.Select(Mapper.Map<PersonnelTrainingCourse, PersonnelTrainingCourseDto>);
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

                var list = courses.Select(Mapper.Map<PersonnelTrainingCourse, GridPersonnelTrainingCourseDto>);

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
        public async Task<IHttpActionResult> UpdateCourse([FromBody] PersonnelTrainingCourse course)
        {
            try
            {
                await _service.UpdateCourse(course);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Course updated");
        }

        [HttpPost]
        [Route("observations/create", Name = "ApiCreateObservation")]
        [RequiresPermission("EditObservations")]
        public async Task<IHttpActionResult> CreateObservation([FromBody] PersonnelObservation data)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _service.CreateObservation(data, userId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Observation created");
        }

        [HttpGet]
        [RequiresPermission("ViewObservations")]
        [Route("observations/get/byId/{observationId:int}", Name = "ApiGetObservationById")]
        public async Task<PersonnelObservationDto> GetObservation([FromUri] int observationId)
        {
            try
            {
                var observation = await _service.GetObservationById(observationId);

                return Mapper.Map<PersonnelObservation, PersonnelObservationDto>(observation);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }  
        
        [HttpGet]
        [RequiresPermission("ViewObservations")]
        [Route("observations/get/byStaff/{staffMemberId:int}", Name = "ApiGetObservationsByStaffMember")]
        public async Task<IEnumerable<PersonnelObservationDto>> GetObservationsByStaffMember([FromUri] int staffMemberId)
        {
            try
            {
                var observations = await _service.GetObservationsByStaffMember(staffMemberId);

                return observations.Select(Mapper.Map<PersonnelObservation, PersonnelObservationDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewObservations")]
        [Route("observations/get/byStaff/dataGrid/{staffMemberId:int}", Name = "ApiGetObservationsByStaffMemberDataGrid")]
        public async Task<IHttpActionResult> GetObservationsForStaffMemberDataGrid([FromUri] int staffMemberId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var observations = await _service.GetObservationsByStaffMember(staffMemberId);

                var list = observations.Select(Mapper.Map<PersonnelObservation, GridPersonnelObservationDto>);

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
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Observation deleted");
        }

        [HttpPost]
        [RequiresPermission("EditObservations")]
        [Route("observations/update", Name = "ApiUpdateObservation")]
        public async Task<IHttpActionResult> UpdateObservation([FromBody] PersonnelObservation observation)
        {
            try
            {
                await _service.UpdateObservation(observation);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Observation updated");
        }
    }
}
