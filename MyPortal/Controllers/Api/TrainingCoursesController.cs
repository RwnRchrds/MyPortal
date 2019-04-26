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
    [Authorize]
    public class TrainingCoursesController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public TrainingCoursesController()
        {
            _context = new MyPortalDbContext();
        }

        [HttpDelete]
        [Route("api/courses/remove/{courseId}")]
        public IHttpActionResult DeleteCourse(int courseId)
        {
            var courseInDb = _context.PersonnelTrainingCourses.Single(x => x.Id == courseId);

            if (courseInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Training course not found");
            }

            if (courseInDb.PersonnelTrainingCertificates.Any())
            {
                return Content(HttpStatusCode.BadRequest, "Cannot delete course that has issued certificates");
            }

            _context.PersonnelTrainingCourses.Remove(courseInDb);
            _context.SaveChanges();

            return Ok("Training course deleted");
        }


        [HttpGet]
        [Route("api/courses/fetch/{courseId}")]
        public PersonnelTrainingCourseDto GetCourse(int courseId)
        {
            var courseInDb = _context.PersonnelTrainingCourses.Single(x => x.Id == courseId);

            if (courseInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<PersonnelTrainingCourse, PersonnelTrainingCourseDto>(courseInDb);
        }

        [HttpGet]
        [Route("api/courses")]
        public IEnumerable<PersonnelTrainingCourseDto> GetCourses()
        {
            return _context.PersonnelTrainingCourses
                .ToList()
                .Select(Mapper.Map<PersonnelTrainingCourse, PersonnelTrainingCourseDto>);
        }

        [HttpPost]
        [Route("api/courses/edit")]
        public IHttpActionResult UpdateCourse(PersonnelTrainingCourse course)
        {
            var courseInDb = _context.PersonnelTrainingCourses.Single(x => x.Id == course.Id);

            if (courseInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Training course not found");
            }

            courseInDb.Code = course.Code;
            courseInDb.Description = course.Description;

            _context.SaveChanges();

            return Ok("Training course updated");
        }
    }
}