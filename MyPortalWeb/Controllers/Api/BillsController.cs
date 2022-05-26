using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/bills")]
    public class BillsController : BaseApiController
    {
        private IBillService _billService;

        public BillsController(IUserService userService, IRoleService roleService, IBillService billService) : base(
            userService, roleService)
        {
            _billService = billService;
        }

        [HttpGet]
        [Route("generate/{chargeBillingPeriodId}")]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.FinanceEditBills)]
        [ProducesResponseType(typeof(IEnumerable<BillModel>), 200)]
        public async Task<IActionResult> GenerateChargeBills([FromRoute] Guid chargeBillingPeriodId)
        {
            try
            {
                var generatedBills = await _billService.GenerateChargeBills(chargeBillingPeriodId);

                return Ok(generatedBills);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
