using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.ListModels;
using MyPortalCore.Areas.Staff.ViewModels.Home;

namespace MyPortalCore.Areas.Staff.Controllers
{
    public class HomeController : StaffPortalController
    {
        private readonly IApplicationUserService _userService;
        private readonly IPersonService _personService;
        private readonly ITaskService _taskService;

        public HomeController(IApplicationUserService userService, IPersonService personService, ITaskService taskService) : base(userService)
        {
            _personService = personService;
            _userService = userService;
            _taskService = taskService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<TaskListModel> tasks = new List<TaskListModel>();
            
            var user = await _userService.GetUserByPrincipal(User);

            var person = await _personService.GetByUserId(user.Id, false);

            var taskTypes = (await _taskService.GetTypes(true)).ToSelectList();

            if (person != null)
            {
                tasks = (await _taskService.GetByPerson(person.Id)).Select(x => x.ToListModel(true));   
            }

            var selectedAcademicYear = await _userService.GetSelectedAcademicYear(user.Id);

            var viewModel = new HomepageViewModel
            {
                SelectedAcademicYear = selectedAcademicYear,
                Person = person,
                TaskTypes = taskTypes,
                Tasks = tasks
            };

            return View(viewModel);
        }

        [Route("Setup")]
        public async Task<IActionResult> Setup([FromServices] ISystemSettingService settingService)
        {
            if (true)
            {
                return View();
            }

            return await Index();
        }
    }
}