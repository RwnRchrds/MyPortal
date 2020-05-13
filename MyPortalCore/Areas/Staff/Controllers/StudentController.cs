using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;
using MyPortal.Logic.Authorisation.Attributes;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortalCore.Areas.Staff.ViewModels.Student;

namespace MyPortalCore.Areas.Staff.Controllers
{
    public class StudentController : BaseController
    {
        private IStudentService _studentService;
        private IPersonService _personService;
        private ILogNoteService _logNoteService;

        public StudentController(IStudentService studentService, IPersonService personService, ILogNoteService logNoteService, IApplicationUserService userService) : base(userService)
        {
            _studentService = studentService;
            _personService = personService;
            _logNoteService = logNoteService;
        }

        [RequiresPermission(Permissions.Student.Details.View)]
        public IActionResult Index()
        {
            var viewModel = new StudentSearchViewModel();
            viewModel.SearchTypes = _studentService.GetSearchFilters();
            viewModel.GenderOptions = _personService.GetGenderOptions();

            return View(viewModel);
        }


        [Route("{studentId}")]
        [RequiresPermission(Permissions.Student.Details.View)]
        public async Task<IActionResult> StudentProfileOverview(Guid studentId)
        {
            var user = await _userService.GetUserByPrincipal(User);

            if (user.SelectedAcademicYearId == null)
            {
                return BadRequest("No academic year has been selected.");
            }

            var viewModel = new StudentOverviewViewModel();

            viewModel.Student = await _studentService.GetById(studentId);
            viewModel.LogNotes =
                (await _logNoteService.GetByStudent(studentId, user.SelectedAcademicYearId.Value)).Select(x =>
                    x.ToListModel());
            viewModel.LogNoteTypes = (await _logNoteService.GetTypes()).ToSelectList();

            return View(viewModel);
        }

        [Route("{studentId}/Documents")]
        [RequiresPermission(Permissions.Student.StudentDocuments.Edit)]
        public async Task<IActionResult> StudentProfileDocuments(Guid studentId)
        {
            var viewModel = new StudentDocumentsViewModel();

            viewModel.Student = await _studentService.GetById(studentId);

            return View(viewModel);
        }
    }
}