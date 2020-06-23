using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortalCore.Areas.Staff.ViewModels.Home;

namespace MyPortalCore.Areas.Staff.Controllers
{
    public class HomeController : StaffPortalController
    {
        private readonly IApplicationUserService _userService;

        public HomeController(IApplicationUserService userService) : base(userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserByPrincipal(User);

            var selectedAcademicYear = await _userService.GetSelectedAcademicYear(user.Id);

            var viewModel = new HomepageViewModel
            {
                SelectedAcademicYear = selectedAcademicYear
            };

            return View(viewModel);
        }
    }
}