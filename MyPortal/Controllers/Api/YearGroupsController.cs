using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                .ToList()
                .Select(Mapper.Map<YearGroup, YearGroupDto>);
        }
    }
}
