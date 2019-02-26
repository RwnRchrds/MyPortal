using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models;

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
        public IHttpActionResult CreateTrainingCertificate(TrainingCertificate trainingCertificateDto)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "Invalid data");

            var userId = User.Identity.GetUserId();

            var userPerson = _context.Staff.SingleOrDefault(x => x.UserId == userId);

            if (trainingCertificateDto.StaffId == userPerson.Id)
                return Content(HttpStatusCode.BadRequest, "Cannot add a certificate for yourself");

            var cert = trainingCertificateDto;

            _context.TrainingCertificates.Add(cert);
            _context.SaveChanges();

            return Ok("Certificate added");
        }

        [HttpDelete]
        [Route("api/staff/certificates/delete/{staff}/{course}")]
        public IHttpActionResult DeleteCertificate(int staff, int course)
        {
            var certInDb =
                _context.TrainingCertificates.SingleOrDefault(l => l.StaffId == staff && l.CourseId == course);

            if (certInDb == null)
                return Content(HttpStatusCode.NotFound, "Certificate not found");

            var userId = User.Identity.GetUserId();

            var userPerson = _context.Staff.SingleOrDefault(x => x.UserId == userId);

            if (staff == userPerson.Id)
                return Content(HttpStatusCode.BadRequest, "Cannot remove a certificate for yourself");

            _context.TrainingCertificates.Remove(certInDb);
            _context.SaveChanges();

            return Ok("Certificate deleted");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/staff/certificates/fetch/{staffId}/{courseId}")]
        public TrainingCertificateDto GetCertificate(int staffId, int courseId)
        {
            var certInDb = _context.TrainingCertificates.Single(x => x.StaffId == staffId && x.CourseId == courseId);

            if (certInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<TrainingCertificate, TrainingCertificateDto>(certInDb);
        }

        [HttpGet]
        [Route("api/staff/certificates/fetch/{staff}")]
        public IEnumerable<TrainingCertificateDto> GetCertificates(int staff)
        {
            var staffInDb = _context.Staff.Single(x => x.Id == staff);

            if (staffInDb == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return _context.TrainingCertificates
                .Where(c => c.StaffId == staff)
                .ToList()
                .Select(Mapper.Map<TrainingCertificate, TrainingCertificateDto>);
        }

        [HttpPost]
        [Route("api/staff/certificates/update")]
        public IHttpActionResult UpdateCertificate(TrainingCertificate data)
        {
            var certInDb =
                _context.TrainingCertificates.Single(x => x.StaffId == data.StaffId && x.CourseId == data.CourseId);

            if (certInDb == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            var userId = User.Identity.GetUserId();

            var userPerson = _context.Staff.SingleOrDefault(x => x.UserId == userId);

            if (userPerson != null && data.StaffId == userPerson.Id)
                return Content(HttpStatusCode.BadRequest, "Cannot modify a certificate for yourself");

            certInDb.StatusId = data.StatusId;

            _context.SaveChanges();

            return Ok("Certificate updated");
        }
    }
}