using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Models.Identity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Authorisation.Attributes;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.DataTables;
using MyPortal.Logic.Models.Student;

namespace MyPortalCore.Controllers.Api
{
    [Authorize]
    public class StudentController : BaseApiController
    {
        private readonly IStudentService _service;
        private RoleManager<ApplicationRole> _roleManager;

        public StudentController(IStudentService service, RoleManager<ApplicationRole> roleManager, IApplicationUserService userService) : base(userService)
        {
            _roleManager = roleManager;
            _service = service;
        }

        [Authorize(Policy = PolicyDictionary.UserType.Staff)]
        [RequiresPermission(PermissionDictionary.Student.Details.View)]
        [Route("Search", Name = "ApiStudentSearch")]
        public async Task<IActionResult> Search([FromQuery]StudentSearchParams searchParams)
        {
            return await Process(async () =>
            {
                var students = await _service.Get(searchParams);

                var result = students.Select(_dTMapper.Map<DataGridStudent>);

                return Ok(result);
            });
        }
    }
}