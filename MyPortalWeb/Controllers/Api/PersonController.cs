using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Response.People;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/people")]
    public class PersonController : BaseApiController
    {
        private IPersonService _personService;

        public PersonController(IUserService userService, IRoleService roleService, IPersonService personService) :
            base(userService, roleService)
        {
            _personService = personService;
        }

        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("search")]
        [ProducesResponseType(typeof(IEnumerable<PersonSearchResultResponseModel>), 200)]
        public async Task<IActionResult> SearchPeople([FromQuery] PersonSearchOptions searchModel)
        {
            try
            {
                var people = await _personService.GetPeopleWithTypes(searchModel);

                return Ok(people);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        
        [HttpGet]
        [Route("user/{userId}")]
        [ProducesResponseType(typeof(PersonModel), 200)]
        public async Task<IActionResult> GetPersonByUser([FromRoute] Guid userId)
        {
            try
            {
                var person = await _personService.GetByUserId(userId, false);

                return Ok(person);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("loggedInUser")]
        [ProducesResponseType(typeof(PersonModel), 200)]
        public async Task<IActionResult> GetPersonByCurrentUser()
        {
            try
            {
                var userId = User.GetUserId();
                
                var person = await GetPersonByUser(userId);

                return Ok(person);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
