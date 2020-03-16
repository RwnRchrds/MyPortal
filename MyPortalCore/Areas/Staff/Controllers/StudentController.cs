using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Logic.Authorisation.Attributes;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;
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

        [RequiresPermission(PermissionDictionary.Student.Details.View)]
        public IActionResult Index()
        {
            var viewModel = new StudentSearchViewModel();
            viewModel.SearchTypes = _service.GetSearchTypes();
            viewModel.GenderOptions = _personService.GetGenderOptions();

            return View(viewModel);
        }

        [RequiresPermission(PermissionDictionary.Student.Details.View)]
        [Route("{studentId}")]
        [RequiresPermission(PermissionDictionary.Student.Details.View)]
        public async Task<IActionResult> StudentOverview(Guid studentId)
        {
            var viewModel = new StudentOverviewViewModel();

            var student = await _service.GetById(studentId);

            viewModel.Student = student;

            return View(viewModel);
        }
    }
}