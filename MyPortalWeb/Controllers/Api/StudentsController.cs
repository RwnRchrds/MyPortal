using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Students;

using MyPortal.Logic.Models.Requests.Student;
using MyPortal.Logic.Models.Summary;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/students")]
    public class StudentsController : PersonalDataController
    {
        private readonly IAcademicYearService _academicYearService;

        public StudentsController(IUserService userService, IPersonService personService,
            IStudentService studentService, IAcademicYearService academicYearService) 
            : base(userService, personService, studentService)
        {
            _academicYearService = academicYearService;
        }

        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.StudentViewStudentDetails)]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<StudentSummaryModel>), 200)]
        public async Task<IActionResult> SearchStudents([FromQuery] StudentSearchOptions searchModel)
        {
            var students = (await StudentService.SearchStudents(searchModel)).ToList();

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
                var student = await StudentService.GetStudentById(studentId);
                
                if (await CanAccessPerson(student.PersonId))
                {
                    return Ok(student);
                }

                return PermissionError();
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
                var student = await StudentService.GetStudentById(studentId);
                
                if (await CanAccessPerson(student.PersonId))
                {
                    if (academicYearId == null || academicYearId == Guid.Empty)
                    {
                        academicYearId = (await _academicYearService.GetCurrentAcademicYear(true)).Id;
                    }

                    if (!academicYearId.HasValue)
                    {
                        return Error(HttpStatusCode.NotFound, "No academic year was found.");
                    }

                    var studentStats = await StudentService.GetStatsById(studentId, academicYearId.Value);

                    return Ok(studentStats);
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}