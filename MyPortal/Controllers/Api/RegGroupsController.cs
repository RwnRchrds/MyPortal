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
    public class RegGroupsController : ApiController
    {
        private MyPortalDbContext _context;

        public RegGroupsController()
        {
            _context = new MyPortalDbContext();
        }

        [Route("api/reggroups/{yearGroup}")]
        public IEnumerable<RegGroupDto> GetRegGroups(int yearGroup)
        {
            return _context.RegGroups
                .Where(x => x.YearGroup == yearGroup)
                .ToList()
                .Select(Mapper.Map<RegGroup, RegGroupDto>);
        }
    }
}
