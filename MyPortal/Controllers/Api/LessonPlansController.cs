using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
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

        /// <summary>
        /// Gets all lesson plans from the database (in alphabetical order).
        /// </summary>
        /// <returns>Returns a list of DTOs of all lesson plans from the database.</returns>
        [HttpGet]
        [Route("api/lessonPlans/all")]
        public IEnumerable<LessonPlanDto> GetLessonPlans()
        {
            return _context.LessonPlans.OrderBy(x => x.Title).ToList().Select(Mapper.Map<LessonPlan, LessonPlanDto>);
        }

        /// <summary>
        /// Gets lesson plan from the database with the specified ID.
        /// </summary>
        /// <param name="id">ID of the lesson plan to fetch from the database.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
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

        /// <summary>
        /// Gets lesson plans from the specified study topic.
        /// </summary>
        /// <param name="id">The ID of the study topic to get lesson plans from.</param>
        /// <returns>Returns a list of DTOs of lesson plans from the specified study topic.</returns>
        [HttpGet]
        [Route("api/lessonPlans/byTopic/{id}")]
        public IEnumerable<LessonPlanDto> GetLessonPlansByTopic(int id)
        {
            return _context.LessonPlans.Where(x => x.StudyTopicId == id).OrderBy(x => x.Title).ToList()
                .Select(Mapper.Map<LessonPlan, LessonPlanDto>);
        }

        /// <summary>
        /// Adds a lesson plan to the database.
        /// </summary>
        /// <param name="plan">The lesson plan to add to the database</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/lessonPlans/create")]
        public IHttpActionResult CreateLessonPlan(LessonPlan plan)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }
            
            var authorId = plan.AuthorId;

            var author = new Staff();

            if (authorId == 0)
            {
                var userId = User.Identity.GetUserId();
                author = _context.Staff.SingleOrDefault(x => x.UserId == userId);
                if (author == null)
                {
                    return Content(HttpStatusCode.BadRequest, "User does not have a personnel profile");
                }
            }

            if (authorId != 0)
            {
                author = _context.Staff.SingleOrDefault(x => x.Id == authorId);
            }

            if (author == null)
            {
                return Content(HttpStatusCode.NotFound, "Staff member not found");
            }

            plan.AuthorId = author.Id;

            _context.LessonPlans.Add(plan);
            _context.SaveChanges();

            return Ok("Lesson plan added");
        }

        /// <summary>
        /// Updates the lesson plan specified.
        /// </summary>
        /// <param name="plan">Lesson plan to update in the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
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

        /// <summary>
        /// Deletes the specified lesson plan from the database.
        /// </summary>
        /// <param name="id">The ID of the lesson plan to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
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