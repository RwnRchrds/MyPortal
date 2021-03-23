using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/bills")]
    public class BillsController : BaseApiController
    {
        public BillsController(IAppServiceCollection services) : base(services)
        {
        }

        [HttpGet]
        [Route("generate")]
        [ProducesResponseType(typeof(IEnumerable<BillModel>), 200)]
        public async Task<IActionResult> GenerateChargeBills()
        {
            return await ProcessAsync(async () =>
            {
                var generatedBills = await Services.Bills.GenerateChargeBills();

                return Ok(generatedBills);
            });
        }
    }
}
