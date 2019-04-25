using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;

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

        [HttpPost]
        [Route("api/subjects/new")]
        public IHttpActionResult CreateSubject(CurriculumSubject data)
        {
            if (data.Name.IsNullOrWhiteSpace() || !ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var subjectToAdd = data;

            _context.CurriculumSubjects.Add(subjectToAdd);
            _context.SaveChanges();
            return Ok("Subject created");
        }

        [HttpDelete]
        [Route("api/subjects/delete/{subjectId}")]
        public IHttpActionResult DeleteSubject(int subjectId)
        {
            var subjectInDb = _context.CurriculumSubjects.SingleOrDefault(x => x.Id == subjectId);

            if (subjectInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Subject not found");
            }

            _context.AssessmentResults.RemoveRange(subjectInDb.AssessmentResults);
            _context.CurriculumSubjects.Remove(subjectInDb);
            _context.SaveChanges();
            return Ok("Subject deleted");
        }

        [HttpGet]
        [Route("api/subjects/byId/{subjectId}")]
        public CurriculumSubjectDto GetSubject(int subjectId)
        {
            var subject = _context.CurriculumSubjects.SingleOrDefault(x => x.Id == subjectId);

            if (subject == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CurriculumSubject, CurriculumSubjectDto>(subject);
        }

        [HttpGet]
        [Route("api/subjects/all")]
        public IEnumerable<CurriculumSubjectDto> GetSubjects()
        {
            return _context.CurriculumSubjects.OrderBy(x => x.Name).ToList().Select(Mapper.Map<CurriculumSubject, CurriculumSubjectDto>);
        }

        [HttpPost]
        [Route("api/subjects/update")]
        public IHttpActionResult UpdateSubject(CurriculumSubject data)
        {
            var subjectInDb = _context.CurriculumSubjects.SingleOrDefault(x => x.Id == data.Id);

            if (subjectInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Subject not found");
            }

            subjectInDb.Name = data.Name;
            subjectInDb.LeaderId = data.LeaderId;
            _context.SaveChanges();
            return Ok("Subject updated");
        }
    }
}