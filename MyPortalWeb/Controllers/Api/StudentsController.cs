using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Response.Students;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/students")]
    public class StudentsController : StudentApiController
    {
        public StudentsController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
        {
        }

        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("search")]
        [ProducesResponseType(typeof(IEnumerable<StudentDataGridModel>), 200)]
        public async Task<IActionResult> SearchStudents([FromQuery] StudentSearchOptions searchModel)
        {
            return await ProcessAsync(async () =>
            {
                IEnumerable<StudentDataGridModel> students;

                students = (await Services.Students.Get(searchModel)).Select(x => x.GetDataGridModel());

                return Ok(students);
            }, Permissions.Student.StudentDetails.ViewStudentDetails);
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(typeof(StudentModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid studentId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthoriseStudent(studentId))
                {
                    var student = await Services.Students.GetById(studentId);

                    return Ok(student);
                }

                return Forbid();
            }, Permissions.Student.StudentDetails.ViewStudentDetails);
        }

        [HttpGet]
        [Route("stats")]
        [ProducesResponseType(typeof(StudentStatsModel), 200)]
        public async Task<IActionResult> GetStatsById([FromQuery] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthoriseStudent(studentId))
                {
                    if (academicYearId == null || academicYearId == Guid.Empty)
                    {
                        academicYearId = (await Services.AcademicYears.GetCurrentAcademicYear()).Id;
                    }

                    var studentStats = await Services.Students.GetStatsById(studentId, academicYearId.Value);

                    return Ok(studentStats);
                }

                return Forbid();
            }, Permissions.Student.StudentDetails.ViewStudentDetails);
        }
    }
}