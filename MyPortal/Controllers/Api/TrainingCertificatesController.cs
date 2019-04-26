using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    public class TrainingCertificatesController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public TrainingCertificatesController()
        {
            _context = new MyPortalDbContext();
        }

        [HttpPost]
        [Route("api/staff/certificates/create")]
        public IHttpActionResult CreateTrainingCertificate(PersonnelTrainingCertificate trainingCertificateDto)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var userId = User.Identity.GetUserId();

            var userPerson = _context.CoreStaff.SingleOrDefault(x => x.UserId == userId);

            if (trainingCertificateDto.StaffId == userPerson.Id)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot add a certificate for yourself");
            }

            var cert = trainingCertificateDto;

            _context.PersonnelTrainingCertificates.Add(cert);
            _context.SaveChanges();

            return Ok("Certificate added");
        }

        [HttpDelete]
        [Route("api/staff/certificates/delete/{staff}/{course}")]
        public IHttpActionResult DeleteCertificate(int staff, int course)
        {
            var certInDb =
                _context.PersonnelTrainingCertificates.SingleOrDefault(l => l.StaffId == staff && l.CourseId == course);

            if (certInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Certificate not found");
            }

            var userId = User.Identity.GetUserId();

            var userPerson = _context.CoreStaff.SingleOrDefault(x => x.UserId == userId);

            if (staff == userPerson.Id)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot remove a certificate for yourself");
            }

            _context.PersonnelTrainingCertificates.Remove(certInDb);
            _context.SaveChanges();

            return Ok("Certificate deleted");
        }


        [HttpGet]
        [Route("api/staff/certificates/fetch/{staffId}/{courseId}")]
        public PersonnelTrainingCertificateDto GetCertificate(int staffId, int courseId)
        {
            var certInDb = _context.PersonnelTrainingCertificates.Single(x => x.StaffId == staffId && x.CourseId == courseId);

            if (certInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<PersonnelTrainingCertificate, PersonnelTrainingCertificateDto>(certInDb);
        }

        [HttpGet]
        [Route("api/staff/certificates/fetch/{staff}")]
        public IEnumerable<PersonnelTrainingCertificateDto> GetCertificates(int staff)
        {
            var staffInDb = _context.CoreStaff.Single(x => x.Id == staff);

            if (staffInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return _context.PersonnelTrainingCertificates
                .Where(c => c.StaffId == staff)
                .ToList()
                .Select(Mapper.Map<PersonnelTrainingCertificate, PersonnelTrainingCertificateDto>);
        }

        [HttpPost]
        [Route("api/staff/certificates/update")]
        public IHttpActionResult UpdateCertificate(PersonnelTrainingCertificate data)
        {
            var certInDb =
                _context.PersonnelTrainingCertificates.Single(x => x.StaffId == data.StaffId && x.CourseId == data.CourseId);

            if (certInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var userId = User.Identity.GetUserId();

            var userPerson = _context.CoreStaff.SingleOrDefault(x => x.UserId == userId);

            if (userPerson != null && data.StaffId == userPerson.Id)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot modify a certificate for yourself");
            }

            certInDb.StatusId = data.StatusId;

            _context.SaveChanges();

            return Ok("Certificate updated");
        }
    }
}