using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Interfaces;
using MyPortalCore.Areas.Staff.ViewModels.Student;

namespace MyPortalCore.Areas.Staff.Controllers
{
    public class StudentController : BaseController
    {
        private IStudentService _service;
        private IPersonService _personService;

        public StudentController(IStudentService service, IPersonService personService)
        {
            _service = service;
            _personService = personService;
        }

        public IActionResult Index()
        {
            var viewModel = new StudentSearchViewModel();
            viewModel.SearchTypes = _service.GetSearchTypes();
            viewModel.GenderOptions = _personService.GetGenderOptions();

            return View(viewModel);
        }

        [Route("{studentId}")]
        public IActionResult StudentOverview(Guid studentId)
        {
            var viewModel = new StudentOverviewViewModel();

            return View(viewModel);
        }
    }
}