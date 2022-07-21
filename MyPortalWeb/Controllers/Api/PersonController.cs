using System;
using System.Collections.Generic;
using System.Net;
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
    public class PersonController : PersonalDataController
    {
        public PersonController(IStudentService studentService, IPersonService personService, IUserService userService,
            IRoleService roleService) : base(studentService, personService, userService, roleService)
        {
        }


        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("search")]
        [ProducesResponseType(typeof(IEnumerable<PersonSearchResultResponseModel>), 200)]
        public async Task<IActionResult> SearchPeople([FromQuery] PersonSearchOptions searchModel)
        {
            try
            {
                var people = await PersonService.GetPeopleWithTypes(searchModel);

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
                var person = await PersonService.GetByUserId(userId, false);

                if (person.Id.HasValue && await CanAccessPerson(person.Id.Value))
                {
                    return Ok(person);
                }

                return Error(HttpStatusCode.Forbidden, PermissionMessage);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
