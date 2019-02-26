using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class SubjectsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public SubjectsController()
        {
            _context = new MyPortalDbContext();
        }

        public SubjectsController(MyPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/subjects/all")]
        public IEnumerable<SubjectDto> GetSubjects()
        {
            return _context.Subjects.OrderBy(x => x.Name).ToList().Select(Mapper.Map<Subject, SubjectDto>);
        }

        [HttpGet]
        [Route("api/subjects/byId/{subjectId}")]
        public SubjectDto GetSubject(int subjectId)
        {
            var subject = _context.Subjects.SingleOrDefault(x => x.Id == subjectId);

            if (subject == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Subject, SubjectDto>(subject);
        }

        [HttpPost]
        [Route("api/subjects/new")]
        public IHttpActionResult CreateSubject(Subject data)
        {
            if (data.Name.IsNullOrWhiteSpace() || !ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "Invalid data");

            var subjectToAdd = data;

            _context.Subjects.Add(subjectToAdd);
            _context.SaveChanges();
            return Ok("Subject created");
        }

        [HttpPost]
        [Route("api/subjects/update")]
        public IHttpActionResult UpdateSubject(Subject data)
        {
            var subjectInDb = _context.Subjects.SingleOrDefault(x => x.Id == data.Id);

            if (subjectInDb == null) return Content(HttpStatusCode.NotFound, "Subject not found");

            subjectInDb.Name = data.Name;
            subjectInDb.LeaderId = data.LeaderId;
            _context.SaveChanges();
            return Ok("Subject updated");
        }

        [HttpDelete]
        [Route("api/subjects/delete/{subjectId}")]
        public IHttpActionResult DeleteSubject(int subjectId)
        {
            var subjectInDb = _context.Subjects.SingleOrDefault(x => x.Id == subjectId);

            if (subjectInDb == null) return Content(HttpStatusCode.NotFound, "Subject not found");

            _context.Results.RemoveRange(subjectInDb.Results);
            _context.Subjects.Remove(subjectInDb);
            _context.SaveChanges();
            return Ok("Subject deleted");
        }
    }
}