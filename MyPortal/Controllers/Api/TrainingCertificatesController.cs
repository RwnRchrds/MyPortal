using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
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

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/staff/certificates/fetch/{staff}")]
        public IEnumerable<TrainingCertificateDto> GetCertificates(int staff)
        {

            var staffInDb = _context.Staff.Single(x => x.Id == staff);

            if (staffInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return _context.TrainingCertificates
                .Where(c => c.StaffId == staff)
                .ToList()
                .Select(Mapper.Map<TrainingCertificate, TrainingCertificateDto>);
        }

        [HttpPost]
        [Route("api/staff/certificates/create")]
        public IHttpActionResult CreateTrainingCertificate(TrainingCertificateDto trainingCertificateDto)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "Invalid data");

            var cert = Mapper.Map<TrainingCertificateDto, TrainingCertificate>(trainingCertificateDto);

            _context.TrainingCertificates.Add(cert);
            _context.SaveChanges();

            return Ok("Certificate added");
        }

        [HttpPost]
        [Route("api/staff/certificates/update")]
        public IHttpActionResult UpdateCertificate(TrainingCertificateDto data)
        {
            var certInDb = _context.TrainingCertificates.Single(x => x.StaffId == data.StaffId && x.CourseId == data.CourseId);

            if (certInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            certInDb.StatusId = data.StatusId;

            _context.SaveChanges();

            return Ok("Certificate updated");
        }

        [HttpDelete]
        [Route("api/staff/certificates/delete/{staff}/{course}")]
        public IHttpActionResult DeleteCertificate(int staff, int course)
        {
            var certInDb = _context.TrainingCertificates.SingleOrDefault(l => l.StaffId == staff && l.CourseId == course);

            if (certInDb == null)
                return Content(HttpStatusCode.NotFound, "Certificate not found");

            _context.TrainingCertificates.Remove(certInDb);
            _context.SaveChanges();

            return Ok("Certificate deleted");
        }
    }
}