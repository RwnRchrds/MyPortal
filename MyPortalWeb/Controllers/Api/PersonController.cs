using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Response.People;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/people")]
    public class PersonController : BaseApiController
    {
        

        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("search")]
        [ProducesResponseType(typeof(IEnumerable<PersonSearchResultModel>), 200)]
        public async Task<IActionResult> SearchPeople([FromQuery] PersonSearchOptions searchModel)
        {
            return await ProcessAsync(async () =>
            {
                var people = await Services.People.GetWithTypes(searchModel);

                return Ok(people);
            }, Permissions.System.Users.EditUsers);
        }

        public PersonController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
        {
        }
    }
}
