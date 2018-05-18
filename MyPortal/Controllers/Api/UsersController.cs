using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Dtos.Identity;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class UsersController : ApiController
    {
        private ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("api/users")]
        public IEnumerable<UserDto> GetUsers()
        {
            return _context.Users.ToList().Select(Mapper.Map<IdentityUser, UserDto>);
        }
    }
}
