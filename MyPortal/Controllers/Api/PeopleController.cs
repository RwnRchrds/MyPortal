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
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Services;
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
            _service = new PeopleService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
        
        [HttpPost]
        [RequiresPermission("ViewMedical, EditStudents")]
        [Route("medical/conditions/get/byPerson/{personId:int}", Name = "ApiGetMedicalConditionsByPersonDataGrid")]
        public async Task<IHttpActionResult> GetMedicalConditionsByPersonDataGrid([FromUri] int personId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var medicalConditions = await _service.GetMedicalConditionsByPerson(personId);

                var list = medicalConditions.Select(_mapper.Map<DataGridPersonConditionDto>);

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
            "ApiGetMedicalDietaryRequirementsByPersonDataGrid")]
        public async Task<IHttpActionResult> GetMedicalDietaryRequirementsByPersonDataGrid([FromUri] int personId,
            [FromBody] DataManagerRequest dm)
        {
            try
            {
                var dietaryRequirements =
                    await _service.GetMedicalDietaryRequirementsByPerson(personId);

                var list = dietaryRequirements.Select(_mapper.Map<DataGridPersonDietaryRequirementDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
