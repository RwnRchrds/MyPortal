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
    public class TrainingCoursesController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public TrainingCoursesController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/courses")]
        public IEnumerable<TrainingCourseDto> GetCourses()
        {
            return _context.TrainingCourses
                .ToList()
                .Select(Mapper.Map<TrainingCourse, TrainingCourseDto>);
        }

        [HttpPost]
        [Route("api/courses/edit")]
        public IHttpActionResult UpdateCourse(TrainingCourseDto course)
        {
            var courseInDb = _context.TrainingCourses.Single(x => x.Id == course.Id);

            if (courseInDb == null)
                return Content(HttpStatusCode.NotFound, "Training course not found");

            courseInDb.Code = course.Code;
            courseInDb.Description = course.Description;

            _context.SaveChanges();

            return Ok("Training course updated");
        }

        [HttpGet]
        [Route("api/courses/fetch/{courseId}")]
        public TrainingCourseDto GetCourse(int courseId)
        {
            var courseInDb = _context.TrainingCourses.Single(x => x.Id == courseId);

            if (courseInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<TrainingCourse, TrainingCourseDto>(courseInDb);
        }

        [HttpDelete]
        [Route("api/courses/remove/{courseId}")]
        public IHttpActionResult DeleteCourse(int courseId)
        {
            var courseInDb = _context.TrainingCourses.Single(x => x.Id == courseId);

            if (courseInDb == null)
                return Content(HttpStatusCode.NotFound, "Training course not found");

            if (courseInDb.TrainingCertificates.Any())
                return Content(HttpStatusCode.BadRequest, "Cannot delete course that has issued certificates");

            _context.TrainingCourses.Remove(courseInDb);
            _context.SaveChanges();

            return Ok("Training course deleted");
        }
    }
}