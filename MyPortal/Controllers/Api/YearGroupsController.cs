using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

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

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/yearGroups/fetch")]
        public IEnumerable<YearGroupDto> GetYearGroups()
        {
            return _context.YearGroups
                .OrderBy(x => x.Id)
                .ToList()
                .Select(Mapper.Map<YearGroup, YearGroupDto>);
        }

        [HttpPost]
        [Route("api/yearGroups/create")]
        public IHttpActionResult CreateYearGroup(YearGroup yearGroup)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.YearGroups.Add(yearGroup);
            return Ok("Year group added");
        }

        [HttpPost]
        [Route("api/yearGroups/update")]
        public IHttpActionResult UpdateYearGroup(YearGroup yearGroup)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var yearGroupInDb = _context.YearGroups.SingleOrDefault(x => x.Id == yearGroup.Id);

            if (yearGroupInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Year group not found");
            }

            yearGroupInDb.Name = yearGroup.Name;
            yearGroupInDb.HeadId = yearGroup.HeadId;

            _context.SaveChanges();

            return Ok("Year group updated");
        }

        [HttpDelete]
        [Route("api/yearGroups/delete/{id}")]
        public IHttpActionResult DeleteYearGroup(int id)
        {
            var yearGroupInDb = _context.YearGroups.SingleOrDefault(x => x.Id == id);

            if (yearGroupInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Year group not found");
            }

            _context.YearGroups.Remove(yearGroupInDb);
            _context.SaveChanges();

            return Ok("Year group deleted");
        }
    }
}