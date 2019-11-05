using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/people")]
    public class PeopleController : MyPortalApiController
    {
        private readonly PeopleService _service;

        public PeopleController()
        {
            _service = new PeopleService(UnitOfWork);
        }
        
        [HttpPost]
        [RequiresPermission("ViewMedical, EditStudents")]
        [Route("medical/conditions/get/byPerson/{personId:int}", Name = "ApiPeopleGetMedicalConditionsByPersonDataGrid")]
        public async Task<IHttpActionResult> GetMedicalConditionsByPersonDataGrid([FromUri] int personId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var medicalConditions = await _service.GetMedicalConditionsByPerson(personId);

                var list = medicalConditions.Select(Mapper.Map<MedicalPersonCondition, GridMedicalPersonConditionDto>);

                return PrepareDataGridObject(list, dm);
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
                    await _service.GetMedicalDietaryRequirementsByPerson(personId);

                var list = dietaryRequirements.Select(Mapper
                    .Map<MedicalPersonDietaryRequirement, GridMedicalPersonDietaryRequirementDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
