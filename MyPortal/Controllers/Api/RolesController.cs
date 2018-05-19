using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Dtos.Identity;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class RolesController : ApiController
    {
        private ApplicationDbContext _identity;

        public RolesController()
        {
            _identity = new ApplicationDbContext();
        }

        [HttpGet]
        [Route("api/roles")]
        public IEnumerable<RoleDto> GetRoles()
        {
            return _identity.Roles.ToList().Select(Mapper.Map<IdentityRole, RoleDto>);
        }
    }
}
