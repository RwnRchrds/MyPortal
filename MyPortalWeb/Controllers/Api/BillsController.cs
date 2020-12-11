using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/bills")]
    public class BillsController : BaseApiController
    {
        private readonly IBillService _billService;

        public BillsController(IUserService userService, IAcademicYearService academicYearService,
            IBillService billService, IRolePermissionsCache rolePermissionsCache) : base(userService,
            academicYearService, rolePermissionsCache)
        {
            _billService = billService;
        }

        [HttpGet]
        [Route("generate")]
        [Produces(typeof(IEnumerable<BillModel>))]
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
