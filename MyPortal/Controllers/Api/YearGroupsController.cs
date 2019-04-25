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
    public class YearGroupsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public YearGroupsController()
        {
            _context = new MyPortalDbContext();
        }

        [HttpPost]
        [Route("api/yearGroups/create")]
        public IHttpActionResult CreateYearGroup(PastoralYearGroup yearGroup)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.PastoralYearGroups.Add(yearGroup);
            return Ok("Year group added");
        }

        [HttpDelete]
        [Route("api/yearGroups/delete/{id}")]
        public IHttpActionResult DeleteYearGroup(int id)
        {
            var yearGroupInDb = _context.PastoralYearGroups.SingleOrDefault(x => x.Id == id);

            if (yearGroupInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Year group not found");
            }

            _context.PastoralYearGroups.Remove(yearGroupInDb);
            _context.SaveChanges();

            return Ok("Year group deleted");
        }

        [HttpGet]
        [Route("api/yearGroups/fetch")]
        public IEnumerable<PastoralYearGroupDto> GetYearGroups()
        {
            return _context.PastoralYearGroups
                .OrderBy(x => x.Id)
                .ToList()
                .Select(Mapper.Map<PastoralYearGroup, PastoralYearGroupDto>);
        }

        [HttpPost]
        [Route("api/yearGroups/update")]
        public IHttpActionResult UpdateYearGroup(PastoralYearGroup yearGroup)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var yearGroupInDb = _context.PastoralYearGroups.SingleOrDefault(x => x.Id == yearGroup.Id);

            if (yearGroupInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Year group not found");
            }

            yearGroupInDb.Name = yearGroup.Name;
            yearGroupInDb.HeadId = yearGroup.HeadId;

            _context.SaveChanges();

            return Ok("Year group updated");
        }
    }
}