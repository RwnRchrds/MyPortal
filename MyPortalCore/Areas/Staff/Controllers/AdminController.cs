using System;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Authorisation.Attributes;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Interfaces;
using MyPortalCore.Areas.Staff.ViewModels.Admin;

namespace MyPortalCore.Areas.Staff.Controllers
{
    public class AdminController : BaseController
    {
        private IApplicationRoleService _applicationRoleService;

        public AdminController(IApplicationRoleService applicationRoleService, IApplicationUserService userService) : base(userService)
        {
            _applicationRoleService = applicationRoleService;
        }

        [Route("Roles")]
        [RequiresPermission(PermissionDictionary.System.Roles.Edit)]
        public IActionResult Roles()
        {
            var viewModel = new RolesViewModel();
            return View(viewModel);
        }
    }
}