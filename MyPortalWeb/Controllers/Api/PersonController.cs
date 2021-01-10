using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Query;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/people")]
    public class PersonController : BaseApiController
    {
        private readonly IPersonService _personService;

        public PersonController(IUserService userService, IAcademicYearService academicYearService, IRolePermissionsCache rolePermissionsCache, IPersonService personService) : base(userService, academicYearService, rolePermissionsCache)
        {
            _personService = personService;
        }

        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("search")]
        [Produces(typeof(IEnumerable<PersonSearchResultModel>))]
        public async Task<IActionResult> SearchPeople([FromQuery] PersonSearchOptions searchModel)
        {
            return await ProcessAsync(async () =>
            {
                var people = await _personService.GetWithTypes(searchModel);

                return Ok(people);
            }, Permissions.System.Users.EditUsers);
        }
    }
}
