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

        [Route("api/certificates/{staff}")]
        public IEnumerable<TrainingCertificateDto> GetCertificates(string staff)
        {
            return _context.TrainingCertificates
                .Where(c => c.Staff == staff)
                .ToList()
                .Select(Mapper.Map<TrainingCertificate, TrainingCertificateDto>);
        }

        [HttpPost]
        public TrainingCertificateDto CreateTrainingCertificate(TrainingCertificateDto trainingCertificateDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var cert = Mapper.Map<TrainingCertificateDto, TrainingCertificate>(trainingCertificateDto);

            _context.TrainingCertificates.Add(cert);
            _context.SaveChanges();

            return trainingCertificateDto;
        }

        [Route("api/certificates/certificate/{staff}/{course}")]
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