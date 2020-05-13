using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Authorisation.Attributes;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Requests.Student;
using MyPortal.Logic.Models.Summary;

namespace MyPortalCore.Controllers.Api
{
    [Authorize]
    public class StudentController : BaseApiController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService, IApplicationUserService userService) : base(userService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("Search", Name = "ApiStudentSearch")]
        [RequiresPermission(Permissions.Student.Details.View)]
        public async Task<IActionResult> SearchStudents([FromQuery] StudentSearchParams searchParams)
        {
            return await Process(async () =>
            {
                var students = (await _studentService.Get(searchParams)).Select(x => x.GetListModel());

                return Ok(students);
            });
        }

        [HttpGet]
        [Route("GetById", Name = "ApiStudentGetById")]
        [RequiresPermission(Permissions.Student.Details.View)]
        public async Task<IActionResult> GetById([FromQuery] Guid studentId)
        {
            return await Process(async () =>
            {
                var student = await _studentService.GetById(studentId);

                return Ok(student);
            });
        }

        public override void Dispose()
        {
            _studentService.Dispose();
        }
    }
}