using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.People;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    public class PersonController : PersonalDataController
    {
        public PersonController(IUserService userService, IPersonService personService, IStudentService studentService)
            : base(userService, personService, studentService)
        {
        }

        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("api/people")]
        [ProducesResponseType(typeof(IEnumerable<PersonSearchResultModel>), 200)]
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
        [Route("api/users/{userId}/person")]
        [ProducesResponseType(typeof(PersonModel), 200)]
        public async Task<IActionResult> GetPersonByUser([FromRoute] Guid userId)
        {
            try
            {
                var person = await PersonService.GetPersonByUserId(userId, false);

                if (person == null)
                {
                    return Ok();
                }

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