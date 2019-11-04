using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Web.Http;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/people")]
    public class PeopleController : MyPortalApiController
    {
        [HttpPost]
        [RequiresPermission("ViewMedical, EditStudents")]
        [Route("medical/conditions/get/byPerson/{personId:int}", Name = "ApiPeopleGetMedicalConditionsByPersonDataGrid")]
        public async Task<IHttpActionResult> GetMedicalConditionsByPersonDataGrid([FromUri] int personId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var medicalConditions = await PeopleService.GetMedicalConditionsByPersonDataGrid(personId, _context);

                return PrepareDataGridObject(medicalConditions, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewMedical, EditStudents")]
        [Route("medical/dietaryRequirements/get/byPerson/{personId:int}", Name =
            "ApiPeopleGetMedicalDietaryRequirementsByPersonDataGrid")]
        public async Task<IHttpActionResult> GetMedicalDietaryRequirementsByPersonDataGrid([FromUri] int personId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var dietaryRequirements =
                    await PeopleService.GetMedicalDietaryRequirementsByPersonDataGrid(personId, _context);

                return PrepareDataGridObject(dietaryRequirements, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
