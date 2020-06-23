using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;

namespace MyPortalCore.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IApplicationUserService _userService;

        public BaseController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        protected async Task<IActionResult> Process(Func<Task<IActionResult>> method, params Guid[] permissionsRequired)
        {
            if (User.HasPermission(permissionsRequired))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                return await method.Invoke();
            }

            return Forbid();
        }

        protected async Task<bool> AuthenticateStudentResource(IStudentService studentService, Guid studentId)
        {
            if (User.IsType(UserTypes.Student))
            {
                var user = await _userService.GetUserByPrincipal(User);

                var student = await studentService.GetByUserId(user.Id);

                if (student.Id == studentId)
                {
                    return true;
                }
            }
            else if (User.IsType(UserTypes.Staff))
            {
                return true;
            }

            return false;
        }
    }
}