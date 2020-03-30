using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;
using MyPortal.Logic.Authorisation.Attributes;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;
using MyPortalCore.Areas.Staff.ViewModels.Student;

namespace MyPortalCore.Areas.Staff.Controllers
{
    public class StudentController : BaseController
    {
        private IStudentService _service;
        private IPersonService _personService;
        private IProfileLogNoteService _logNoteService;

        public StudentController(IStudentService service, IPersonService personService, IProfileLogNoteService logNoteService, IApplicationUserService userService) : base(userService)
        {
            _service = service;
            _personService = personService;
            _logNoteService = logNoteService;
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
            var user = await _userService.GetUserByPrincipal(User);

            if (user.SelectedAcademicYearId == null)
            {
                return BadRequest("No academic year has been selected.");
            }

            var viewModel = new StudentOverviewViewModel(_service, _personService, _logNoteService, studentId,
                (Guid) user.SelectedAcademicYearId);

            await viewModel.LoadData();

            return View(viewModel);
        }
    }
}