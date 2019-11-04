using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Models.Database;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/personnel")]
    [Authorize]
    public class PersonnelController : MyPortalApiController
    {
        [HttpPost]
        [RequiresPermission("EditTrainingCertificates")]
        [Route("certificates/create", Name = "ApiPersonnelCreateTrainingCertificate")]
        public IHttpActionResult CreateTrainingCertificate([FromBody] PersonnelTrainingCertificate certificate)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PersonnelService.CreateTrainingCertificate(certificate, userId, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditTrainingCertificates")]
        [Route("certificates/delete/{staffId:int}/{courseId:int}", Name = "ApiPersonnelDeleteTrainingCertificate")]
        public IHttpActionResult DeleteCertificate([FromUri] int staffId, [FromUri] int courseId)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PersonnelService.DeleteTrainingCertificate(staffId, courseId, userId, _context));
        }
        
        [HttpGet]
        [RequiresPermission("ViewTrainingCertificates")]
        [Route("certificates/get/{staffId:int}/{courseId:int}", Name = "ApiPersonnelGetTrainingCertificate")]
        public PersonnelTrainingCertificateDto GetTrainingCertificate([FromUri] int staffId, [FromUri] int courseId)
        {
            return PrepareResponseObject(PersonnelService.GetCertificate(staffId, courseId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewTrainingCertificates")]
        [Route("certificates/get/byStaff/{staffId:int}", Name = "ApiPersonnelGetTrainingCertificatesByStaffMember")]
        public IEnumerable<PersonnelTrainingCertificateDto> GetCertificatesByStaffMember([FromUri] int staffId)
        {
            return PrepareResponseObject(PersonnelService.GetCertificatesByStaffMember(staffId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewTrainingCertificates")]
        [Route("certificates/get/byStaff/dataGrid/{staffId:int}", Name = "ApiPersonnelGetTrainingCertificatesByStaffMemberDataGrid")]
        public IHttpActionResult GetCertificatesForStaffMemberDataGrid([FromUri] int staffId, [FromBody] DataManagerRequest dm)
        {
            var certs = PrepareResponseObject(PersonnelService.GetCertificatesForStaffMember_DataGrid(staffId, _context));

            return PrepareDataGridObject(certs, dm);
        }

        [HttpPost]
        [RequiresPermission("EditTrainingCertificates")]
        [Route("certificates/update", Name = "ApiPersonnelUpdateTrainingCertificate")]
        public IHttpActionResult UpdateCertificate([FromBody] PersonnelTrainingCertificate certificate)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PersonnelService.UpdateCertificate(certificate, userId, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditTrainingCourses")]
        [Route("courses/remove/{courseId:int}", Name = "ApiPersonnelDeleteTrainingCourse")]
        public IHttpActionResult DeleteCourse([FromUri] int courseId)
        {
            return PrepareResponse(PersonnelService.DeleteCourse(courseId, _context));
        }

        [HttpGet]
        [Route("courses/get/byId/{courseId:int}", Name = "ApiPersonnelGetTrainingCourseById")]
        [RequiresPermission("ViewTrainingCourses")]
        public PersonnelTrainingCourseDto GetCourseById([FromUri] int courseId)
        {
            return PrepareResponseObject(PersonnelService.GetCourseById(courseId, _context));
        }

        [HttpGet]
        [Route("courses/get/all", Name = "ApiPersonnelGetAllTrainingCourses")]
        [RequiresPermission("ViewTrainingCourses")]
        public IEnumerable<PersonnelTrainingCourseDto> GetCourses()
        {
            return PrepareResponseObject(PersonnelService.GetAllTrainingCourses(_context));
        }

        [HttpPost]
        [Route("courses/get/dataGrid/all", Name = "ApiPersonnelGetAllTrainingCoursesDataGrid")]
        [RequiresPermission("ViewTrainingCourses")]
        public IHttpActionResult GetAllTrainingCourseDataGrid([FromBody] DataManagerRequest dm)
        {
            var trainingCourses = PrepareResponseObject(PersonnelService.GetAllTrainingCourses_DataGrid(_context));

            return PrepareDataGridObject(trainingCourses, dm);
        }

        [HttpPost]
        [Route("courses/edit", Name = "ApiPersonnelUpdateTrainingCourse")]
        [RequiresPermission("EditTrainingCourses")]
        public IHttpActionResult UpdateCourse([FromBody] PersonnelTrainingCourse course)
        {
            return PrepareResponse(PersonnelService.UpdateCourse(course, _context));
        }

        [HttpPost]
        [Route("observations/create", Name = "ApiPersonnelCreateObservation")]
        [RequiresPermission("EditObservations")]
        public IHttpActionResult CreateObservation([FromBody] PersonnelObservation data)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PersonnelService.CreateObservation(data, userId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewObservations")]
        [Route("observations/get/byId/{observationId:int}", Name = "ApiPersonnelGetObservationById")]
        public PersonnelObservationDto GetObservation([FromUri] int observationId)
        {
            return PrepareResponseObject(PersonnelService.GetObservationById(observationId, _context));
        }  
        
        [HttpGet]
        [RequiresPermission("ViewObservations")]
        [Route("observations/get/byStaff/{staffMemberId:int}", Name = "ApiPersonnelGetObservationsByStaffMember")]
        public IEnumerable<PersonnelObservationDto> GetObservationsByStaffMember([FromUri] int staffMemberId)
        {
            return PrepareResponseObject(PersonnelService.GetObservationsByStaffMember(staffMemberId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewObservations")]
        [Route("observations/get/byStaff/dataGrid/{staffMemberId:int}", Name = "ApiPersonnelGetObservationsByStaffMemberDataGrid")]
        public IHttpActionResult GetObservationsForStaffMemberDataGrid([FromUri] int staffMemberId,
            [FromBody] DataManagerRequest dm)
        {
            var observations =
                PrepareResponseObject(
                    PersonnelService.GetObservationsForStaffMember_DataGrid(staffMemberId, _context));

            return PrepareDataGridObject(observations, dm);
        }

        [HttpDelete]
        [RequiresPermission("EditObservations")]
        [Route("observations/delete/{observationId:int}", Name = "ApiPersonnelDeleteObservation")]
        public IHttpActionResult RemoveObservation([FromUri] int observationId)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PersonnelService.DeleteObservation(observationId, userId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditObservations")]
        [Route("observations/update", Name = "ApiPersonnelUpdateObservation")]
        public IHttpActionResult UpdateObservation([FromBody] PersonnelObservation observation)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PersonnelService.UpdateObservation(observation, userId, _context));
        }
    }
}
