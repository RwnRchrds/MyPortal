using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/bills")]
    public class BillsController : BaseApiController
    {
        private readonly IBillService _billService;

        public BillsController(IUserService userService, IAcademicYearService academicYearService, IBillService billService) : base(userService, academicYearService)
        {
            _billService = billService;
        }

        [HttpGet]
        [Route("generate")]
        public async Task<IActionResult> GenerateChargeBills()
        {
            return await ProcessAsync(async () =>
            {
                var generatedBills = await _billService.GenerateChargeBills();

                return Ok(generatedBills);
            });
        }
    }
}
