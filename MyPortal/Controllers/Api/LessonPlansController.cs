using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class LessonPlansController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public LessonPlansController()
        {
            _context = new MyPortalDbContext();
        }

        public LessonPlansController(MyPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/lessonPlans/all")]
        public IEnumerable<LessonPlanDto> GetLessonPlans()
        {
            return _context.LessonPlans.OrderBy(x => x.Title).ToList().Select(Mapper.Map<LessonPlan, LessonPlanDto>);
        }

        [HttpGet]
        [Route("api/lessonPlans/byId/{id}")]
        public LessonPlanDto GetLessonPlanById(int id)
        {
            var lessonPlan = _context.LessonPlans.SingleOrDefault(x => x.Id == id);

            if (lessonPlan == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<LessonPlan, LessonPlanDto>(lessonPlan);
        }

        [HttpGet]
        [Route("api/lessonPlans/byTopic/{id}")]
        public IEnumerable<LessonPlanDto> GetLessonPlansByTopic(int id)
        {
            return _context.LessonPlans.Where(x => x.StudyTopicId == id).OrderBy(x => x.Title).ToList()
                .Select(Mapper.Map<LessonPlan, LessonPlanDto>);
        }

        [HttpPost]
        [Route("api/lessonPlans/create")]
        public IHttpActionResult CreateLessonPlan(LessonPlan plan)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.LessonPlans.Add(plan);
            _context.SaveChanges();

            return Ok("Lesson plan added");
        }

        [HttpPost]
        [Route("api/lessonPlans/update")]
        public IHttpActionResult UpdateLessonPlan(LessonPlan plan)
        {
            var planInDb = _context.LessonPlans.SingleOrDefault(x => x.Id == plan.Id);

            if (planInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Lesson plan not found");
            }

            planInDb.Title = plan.Title;
            planInDb.PlanContent = plan.PlanContent;
            planInDb.StudyTopicId = plan.StudyTopicId;
            planInDb.LearningObjectives = plan.LearningObjectives;
            planInDb.Homework = plan.Homework;

            _context.SaveChanges();

            return Ok("Lesson plan updated");
        }

        [HttpDelete]
        [Route("api/lessonPlans/delete/{id}")]
        public IHttpActionResult DeleteLessonPlan(int id)
        {
            var plan = _context.LessonPlans.SingleOrDefault(x => x.Id == id);

            if (plan == null)
            {
                return Content(HttpStatusCode.NotFound, "Lesson plan not found");
            }

            _context.LessonPlans.Remove(plan);
            _context.SaveChanges();

            return Ok("Lesson plan deleted");
        }
    }
}