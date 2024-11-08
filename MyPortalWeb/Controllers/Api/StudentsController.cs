using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Students;
using MyPortal.Logic.Models.Summary;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/students")]
    public class StudentsController : BaseApiController
    {
        private readonly IAcademicYearService _academicYearService;
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService, IAcademicYearService academicYearService)
        {
            _studentService = studentService;
            _academicYearService = academicYearService;
        }

        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.StudentViewStudentDetails)]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<StudentSummaryModel>), 200)]
        public async Task<IActionResult> SearchStudents([FromQuery] StudentSearchOptions searchModel)
        {
            var students = (await _studentService.SearchStudents(searchModel)).ToList();

            return Ok(students);
        }

        [HttpGet]
        [Route("{studentId}")]
        [Permission(PermissionValue.StudentViewStudentDetails)]
        [ProducesResponseType(typeof(StudentModel), 200)]
        public async Task<IActionResult> GetById([FromRoute] Guid studentId)
        {
            try
            {
                var student = await _studentService.GetStudentById(studentId);

                return Ok(student);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("{studentId}/stats")]
        [Permission(PermissionValue.StudentViewStudentDetails)]
        [ProducesResponseType(typeof(StudentStatsModel), 200)]
        public async Task<IActionResult> GetStatsById([FromRoute] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            try
            {
                if (academicYearId == null || academicYearId == Guid.Empty)
                {
                    academicYearId = (await _academicYearService.GetCurrentAcademicYear(true)).Id;
                }

                if (!academicYearId.HasValue)
                {
                    return Error(HttpStatusCode.NotFound, "No academic year was found.");
                }

                var studentStats = await _studentService.GetStatsByStudentId(studentId, academicYearId.Value);

                return Ok(studentStats);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}