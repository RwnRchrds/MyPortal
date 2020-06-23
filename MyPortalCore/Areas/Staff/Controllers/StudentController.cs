using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;
using MyPortal.Database.Constants;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortalCore.Areas.Staff.ViewModels.Student;

namespace MyPortalCore.Areas.Staff.Controllers
{
    public class StudentController : StaffPortalController
    {
        private IStudentService _studentService;
        private IPersonService _personService;
        private ILogNoteService _logNoteService;
        private IDocumentService _documentService;
        private IAchievementService _achievementService;
        private IAttendanceMarkService _attendanceMarkService;

        public StudentController(IStudentService studentService, IPersonService personService, ILogNoteService logNoteService, IApplicationUserService userService, IDocumentService documentService, IAchievementService achievementService, IAttendanceMarkService attendanceMarkService) : base(userService)
        {
            _studentService = studentService;
            _personService = personService;
            _logNoteService = logNoteService;
            _documentService = documentService;
            _achievementService = achievementService;
            _attendanceMarkService = attendanceMarkService;
        }

        
        public async Task<IActionResult> Index()
        {
            return await Process(async () =>
            {
                var viewModel = new StudentSearchViewModel();
                viewModel.SearchTypes = _studentService.GetSearchFilters();
                viewModel.GenderOptions = _personService.GetGenderOptions();

                return View(viewModel);
            });
        }


        [Route("{studentId}")]
        public async Task<IActionResult> StudentProfileOverview(Guid studentId)
        {
            var user = await _userService.GetUserByPrincipal(User);

            if (user.SelectedAcademicYearId == null)
            {
                return BadRequest("No academic year has been selected.");
            }

            var academicYearId = (Guid)user.SelectedAcademicYearId;

            var viewModel = new StudentOverviewViewModel();

            viewModel.Student = await _studentService.GetById(studentId);
            viewModel.LogNotes =
                (await _logNoteService.GetByStudent(studentId, academicYearId)).Select(x =>
                    x.ToListModel());
            viewModel.LogNoteTypes = (await _logNoteService.GetTypes()).ToSelectList();
            viewModel.AchievementPoints = await _achievementService.GetPointsByStudent(studentId, academicYearId);

            var attendanceSummary = await _attendanceMarkService.GetSummaryByStudent(studentId, academicYearId, true);

            if (attendanceSummary.Valid)
            {
                viewModel.Attendance = attendanceSummary.GetPresentAndApproved();
            }

            return View(viewModel);
        }

        [Route("{studentId}/Documents")]
        public async Task<IActionResult> StudentProfileDocuments(Guid studentId)
        {
            var viewModel = new StudentDocumentsViewModel();

            viewModel.Student = await _studentService.GetById(studentId);
            viewModel.DocumentTypes =
                (await _documentService.GetTypes(SearchFilters.DocumentTypes.Student)).ToSelectList();

            return View(viewModel);
        }
    }
}