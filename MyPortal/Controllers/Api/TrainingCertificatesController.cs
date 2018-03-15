using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class TrainingCertificatesController : ApiController
    {
        private MyPortalDbContext _context;

        public TrainingCertificatesController()
        {
            _context = new MyPortalDbContext();
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
    }
}
