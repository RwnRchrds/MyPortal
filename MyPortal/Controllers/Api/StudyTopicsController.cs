using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;

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

        [HttpPost]
        [Route("api/studyTopics/create")]
        public IHttpActionResult CreateStudyTopic(CurriculumStudyTopic studyTopic)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.CurriculumStudyTopics.Add(studyTopic);
            _context.SaveChanges();

            return Ok("Study topic added");
        }

        [HttpDelete]
        [Route("api/studyTopics/delete/{id}")]
        public IHttpActionResult DeleteStudyTopic(int id)
        {
            var studyTopic = _context.CurriculumStudyTopics.SingleOrDefault(x => x.Id == id);

            if (studyTopic == null)
            {
                return Content(HttpStatusCode.NotFound, "Study topic not found");
            }

            _context.CurriculumStudyTopics.Remove(studyTopic);
            _context.SaveChanges();

            return Ok("Study topic deleted");
        }

        [HttpGet]
        [Route("api/studyTopics/hasLessonPlans/{id}")]
        public bool HasLessonPlans(int id)
        {
            var studyTopic = _context.CurriculumStudyTopics.SingleOrDefault(x => x.Id == id);

            if (studyTopic == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return studyTopic.LessonPlans.Any();
        }
        
        [HttpGet]
        [Route("api/studyTopics/fetch/byId/{id}")]
        public CurriculumStudyTopicDto GetStudyTopic(int id)
        {
            var studyTopic = _context.CurriculumStudyTopics.SingleOrDefault(x => x.Id == id);

            if (studyTopic == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CurriculumStudyTopic, CurriculumStudyTopicDto>(studyTopic);
        }

        [HttpGet]
        [Route("api/studyTopics/fetch/all")]
        public IEnumerable<CurriculumStudyTopicDto> GetStudyTopics()
        {
            return _context.CurriculumStudyTopics.ToList().Select(Mapper.Map<CurriculumStudyTopic, CurriculumStudyTopicDto>);
        }

        [HttpPost]
        [Route("api/studyTopics/update")]
        public IHttpActionResult UpdateStudyTopic(CurriculumStudyTopic studyTopic)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var studyTopicInDb = _context.CurriculumStudyTopics.SingleOrDefault(x => x.Id == studyTopic.Id);

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
    }
}