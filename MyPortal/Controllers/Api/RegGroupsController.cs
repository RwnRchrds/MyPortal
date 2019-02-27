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
    public class RegGroupsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public RegGroupsController()
        {
            _context = new MyPortalDbContext();
        }

        public RegGroupsController(MyPortalDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/regGroups/create")]
        public IHttpActionResult CreateRegGroup(RegGroup regGroup)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.RegGroups.Add(regGroup);
            _context.SaveChanges();

            return Ok("Reg group created");
        }

        [HttpDelete]
        [Route("api/regGroups/delete/{id}")]
        public IHttpActionResult DeleteRegGroup(int id)
        {
            var regGroupInDb = _context.RegGroups.SingleOrDefault(x => x.Id == id);

            if (regGroupInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Reg group not found");
            }

            _context.RegGroups.Remove(regGroupInDb);
            _context.SaveChanges();

            return Ok("Reg group deleted");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/regGroups/byId/{id}")]
        public RegGroupDto GetRegGroup(int id)
        {
            var regGroup = _context.RegGroups.SingleOrDefault(x => x.Id == id);

            if (regGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<RegGroup, RegGroupDto>(regGroup);
        }

        [HttpGet]
        [Route("api/regGroups/byYearGroup/{yearGroup}")]
        public IEnumerable<RegGroupDto> GetRegGroups(int yearGroup)
        {
            return _context.RegGroups
                .Where(x => x.YearGroupId == yearGroup)
                .ToList()
                .Select(Mapper.Map<RegGroup, RegGroupDto>);
        }

        [HttpGet]
        [Route("api/regGroups/all")]
        public IEnumerable<RegGroupDto> GetRegGroups()
        {
            return _context.RegGroups.ToList().Select(Mapper.Map<RegGroup, RegGroupDto>);
        }

        [HttpGet]
        [Route("api/regGroups/hasStudents/{id}")]
        public bool RegGroupHasStudents(int id)
        {
            var regGroupInDb = _context.RegGroups.SingleOrDefault(x => x.Id == id);

            if (regGroupInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return regGroupInDb.Students.Any();
        }

        [HttpPost]
        [Route("api/regGroups/update")]
        public IHttpActionResult UpdateRegGroup(RegGroup regGroup)
        {
            var regGroupInDb = _context.RegGroups.SingleOrDefault(x => x.Id == regGroup.Id);

            if (regGroupInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Reg group not found");
            }

            regGroupInDb.Name = regGroup.Name;
            regGroupInDb.TutorId = regGroup.TutorId;

            _context.SaveChanges();

            return Ok("Reg group updated");
        }
    }
}