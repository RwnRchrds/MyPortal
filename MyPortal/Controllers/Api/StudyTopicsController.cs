using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class StudyTopicsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public StudyTopicsController()
        {
            _context = new MyPortalDbContext();
        }

        public StudyTopicsController(MyPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/studyTopics/fetch/all")]
        public IEnumerable<StudyTopicDto> GetStudyTopics()
        {
            return _context.StudyTopics.ToList().Select(Mapper.Map<StudyTopic, StudyTopicDto>);
        }

        [HttpGet]
        [Route("api/studyTopics/fetch/byId/{id}")]
        public StudyTopicDto GetStudyTopic(int id)
        {
            var studyTopic = _context.StudyTopics.SingleOrDefault(x => x.Id == id);

            if (studyTopic == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<StudyTopic, StudyTopicDto>(studyTopic);
        }

        [HttpPost]
        [Route("api/studyTopics/create")]
        public IHttpActionResult CreateStudyTopic(StudyTopic studyTopic)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.StudyTopics.Add(studyTopic);
            _context.SaveChanges();

            return Ok("Study topic added");
        }

        [HttpPost]
        [Route("api/studyTopics/update")]
        public IHttpActionResult UpdateStudyTopic(StudyTopic studyTopic)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var studyTopicInDb = _context.StudyTopics.SingleOrDefault(x => x.Id == studyTopic.Id);

            if (studyTopicInDb == null)
            {
                return Content(HttpStatusCode.BadRequest, "Study topic not found");
            }

            studyTopicInDb.Name = studyTopic.Name;
            studyTopicInDb.SubjectId = studyTopic.SubjectId;
            studyTopicInDb.YearGroupId = studyTopic.YearGroupId;

            _context.SaveChanges();

            return Ok("Study topic updated");
        }

        [HttpDelete]
        [Route("api/studyTopics/delete/{id}")]
        public IHttpActionResult DeleteStudyTopic(int id)
        {
            var studyTopic = _context.StudyTopics.SingleOrDefault(x => x.Id == id);

            if (studyTopic == null)
            {
                return Content(HttpStatusCode.NotFound, "Study topic not found");
            }

            _context.StudyTopics.Remove(studyTopic);
            _context.SaveChanges();

            return Ok("Study topic deleted");
        }
    }
}