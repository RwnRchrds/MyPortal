using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Collection;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Response.Students;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/students")]
    public class StudentsController : StudentDataController
    {
        private IAcademicYearService _academicYearService;
        
        public StudentsController(IAcademicYearService academicYearService, IStudentService studentService, IUserService userService, IRoleService roleService) :
            base(studentService, userService, roleService)
        {
            _academicYearService = academicYearService;
        }

        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.StudentViewStudentDetails)]
        [Route("search")]
        [ProducesResponseType(typeof(IEnumerable<StudentCollectionModel>), 200)]
        public async Task<IActionResult> SearchStudents([FromQuery] StudentSearchOptions searchModel)
        {
            var students = (await StudentService.Get(searchModel));
                //.Select(x => new StudentCollectionModel(x));

            return Ok(students);
        }

        [HttpGet]
        [Route("id")]
        [Permission(PermissionValue.StudentViewStudentDetails)]
        [ProducesResponseType(typeof(StudentModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid studentId)
        {
            try
            {
                if (await AuthoriseStudent(studentId))
                {
                    var student = await StudentService.GetById(studentId);

                    return Ok(student);
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("stats")]
        [Permission(PermissionValue.StudentViewStudentDetails)]
        [ProducesResponseType(typeof(StudentStatsModel), 200)]
        public async Task<IActionResult> GetStatsById([FromQuery] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            try
            {
                if (await AuthoriseStudent(studentId))
                {
                    if (academicYearId == null || academicYearId == Guid.Empty)
                    {
                        academicYearId = (await _academicYearService.GetCurrentAcademicYear()).Id;
                    }

                    var studentStats = await StudentService.GetStatsById(studentId, academicYearId.Value);

                    return Ok(studentStats);
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}