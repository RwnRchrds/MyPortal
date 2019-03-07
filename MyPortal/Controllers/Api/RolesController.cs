using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Dtos.Identity;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    public class RolesController : ApiController
    {
        private readonly ApplicationDbContext _identity;

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