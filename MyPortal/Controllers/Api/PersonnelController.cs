using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models.Attributes;
using MyPortal.Models.Database;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/personnel")]
    [Authorize]
    public class PersonnelController : MyPortalApiController
    {
        [HttpPost]
        [RequiresPermission("EditTrainingCertificates")]
        [Route("certificates/create")]
        public IHttpActionResult CreateTrainingCertificate([FromBody] PersonnelTrainingCertificate certificate)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PersonnelProcesses.CreateTrainingCertificate(certificate, userId, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditTrainingCertificates")]
        [Route("certificates/delete/{staffId:int}/{courseId:int}")]
        public IHttpActionResult DeleteCertificate([FromUri] int staffId, [FromUri] int courseId)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PersonnelProcesses.DeleteTrainingCertificate(staffId, courseId, userId, _context));
        }
        
        [HttpGet]
        [RequiresPermission("ViewTrainingCertificates")]
        [Route("certificates/get/{staffId:int}/{courseId:int}")]
        public PersonnelTrainingCertificateDto GetCertificate([FromUri] int staffId, [FromUri] int courseId)
        {
            return PrepareResponseObject(PersonnelProcesses.GetCertificate(staffId, courseId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewTrainingCertificates")]
        [Route("certificates/get/byStaff/{staffId:int}")]
        public IEnumerable<PersonnelTrainingCertificateDto> GetCertificatesForStaffMember([FromUri] int staffId)
        {
            return PrepareResponseObject(PersonnelProcesses.GetCertificatesForStaffMember(staffId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewTrainingCertificates")]
        [Route("certificates/get/byStaff/dataGrid/{staffId:int}")]
        public IHttpActionResult GetCertificatesForStaffMemberDataGrid([FromUri] int staffId, [FromBody] DataManagerRequest dm)
        {
            var certs = PrepareResponseObject(PersonnelProcesses.GetCertificatesForStaffMember_DataGrid(staffId, _context));

            return PrepareDataGridObject(certs, dm);
        }

        [HttpPost]
        [RequiresPermission("EditTrainingCertificates")]
        [Route("certificates/update")]
        public IHttpActionResult UpdateCertificate([FromBody] PersonnelTrainingCertificate certificate)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PersonnelProcesses.UpdateCertificate(certificate, userId, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditTrainingCourses")]
        [Route("courses/remove/{courseId:int}")]
        public IHttpActionResult DeleteCourse([FromUri] int courseId)
        {
            return PrepareResponse(PersonnelProcesses.DeleteCourse(courseId, _context));
        }

        [HttpGet]
        [Route("courses/get/byId/{courseId:int}")]
        [RequiresPermission("ViewTrainingCourses")]
        public PersonnelTrainingCourseDto GetCourseById([FromUri] int courseId)
        {
            return PrepareResponseObject(PersonnelProcesses.GetCourseById(courseId, _context));
        }

        [HttpGet]
        [Route("courses/get/all")]
        [RequiresPermission("ViewTrainingCourses")]
        public IEnumerable<PersonnelTrainingCourseDto> GetCourses()
        {
            return PrepareResponseObject(PersonnelProcesses.GetAllTrainingCourses(_context));
        }

        [HttpPost]
        [Route("courses/get/dataGrid/all")]
        [RequiresPermission("ViewTrainingCourses")]
        public IHttpActionResult GetAllTrainingCourseForDataGrid([FromBody] DataManagerRequest dm)
        {
            var trainingCourses = PrepareResponseObject(PersonnelProcesses.GetAllTrainingCourses_DataGrid(_context));

            return PrepareDataGridObject(trainingCourses, dm);
        }

        [HttpPost]
        [Route("courses/edit")]
        [RequiresPermission("EditTrainingCourses")]
        public IHttpActionResult UpdateCourse([FromBody] PersonnelTrainingCourse course)
        {
            return PrepareResponse(PersonnelProcesses.UpdateCourse(course, _context));
        }

        [HttpPost]
        [Route("observations/create")]
        [RequiresPermission("EditObservations")]
        public IHttpActionResult CreateObservation([FromBody] PersonnelObservation data)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PersonnelProcesses.CreateObservation(data, userId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewObservations")]
        [Route("observations/get/byId/{observationId:int}")]
        public PersonnelObservationDto GetObservation([FromUri] int observationId)
        {
            return PrepareResponseObject(PersonnelProcesses.GetObservationById(observationId, _context));
        }  
        
        [HttpGet]
        [RequiresPermission("ViewObservations")]
        [Route("observations/get/byStaff/{staffMemberId:int}")]
        public IEnumerable<PersonnelObservationDto> GetObservationsForStaffMember([FromUri] int staffMemberId)
        {
            return PrepareResponseObject(PersonnelProcesses.GetObservationsForStaffMember(staffMemberId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewObservations")]
        [Route("observations/get/byStaff/dataGrid/{staffMemberId:int}")]
        public IHttpActionResult GetObservationsForStaffMemberDataGrid([FromUri] int staffMemberId,
            [FromBody] DataManagerRequest dm)
        {
            var observations =
                PrepareResponseObject(
                    PersonnelProcesses.GetObservationsForStaffMember_DataGrid(staffMemberId, _context));

            return PrepareDataGridObject(observations, dm);
        }

        [HttpDelete]
        [RequiresPermission("EditObservations")]
        [Route("observations/delete/{observationId:int}")]
        public IHttpActionResult RemoveObservation([FromUri] int observationId)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PersonnelProcesses.DeleteObservation(observationId, userId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditObservations")]
        [Route("observations/update")]
        public IHttpActionResult UpdateObservation([FromBody] PersonnelObservation observation)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(PersonnelProcesses.UpdateObservation(observation, userId, _context));
        }
    }
}
