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
        public IEnumerable<TrainingCertificateDto> GetCertificates(string staff)
        {

            var staffInDb = _context.Staff.Single(x => x.Code == staff);

            if (staffInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return _context.TrainingCertificates
                .Where(c => c.Staff == staff)
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
            var certInDb = _context.TrainingCertificates.Single(x => x.Staff == data.Staff && x.Course == data.Course);

            if (certInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            certInDb.Status = data.Status;

            _context.SaveChanges();

            return Ok("Certificate updated");
        }

        [HttpDelete]
        [Route("api/staff/certificates/delete/{staff}/{course}")]
        public IHttpActionResult DeleteCertificate(string staff, int course)
        {
            var certInDb = _context.TrainingCertificates.SingleOrDefault(l => l.Staff == staff && l.Course == course);

            if (certInDb == null)
                return Content(HttpStatusCode.NotFound, "Certificate not found");

            _context.TrainingCertificates.Remove(certInDb);
            _context.SaveChanges();

            return Ok("Certificate deleted");
        }
    }
}