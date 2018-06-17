using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
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

        [Route("api/courses")]
        public IEnumerable<TrainingCourseDto> GetCourses()
        {
            return _context.TrainingCourses
                .ToList()
                .Select(Mapper.Map<TrainingCourse, TrainingCourseDto>);
        }
    }
}