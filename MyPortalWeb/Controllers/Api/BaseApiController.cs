using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Database.Constants;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Exceptions;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase, IDisposable
    {
        protected readonly IUserService UserService;
        protected readonly IAcademicYearService AcademicYearService;

        public BaseApiController(IUserService userService, IAcademicYearService academicYearService)
        {
            UserService = userService;
            AcademicYearService = academicYearService;
        }

        public virtual void Dispose()
        {
            UserService.Dispose();
            AcademicYearService.Dispose();
        }

        protected async Task<IActionResult> ProcessAsync(Func<Task<IActionResult>> method, params Guid[] permissionsRequired)
        {
            if (User.HasPermission(permissionsRequired))
            {
                try
                {
                    return await method.Invoke();
                }
                catch (Exception e)
                {
                    return HandleException(e);
                }
            }

            return Forbid();
        }
        
        protected async Task<Guid> GetCurrentAcademicYearId()
        {
            var currentYear = await AcademicYearService.GetCurrent();

            return currentYear.Id;
        }

        private IActionResult HandleException(Exception ex)
        {
            var statusCode = HttpStatusCode.BadRequest;

            var message = ExceptionHelper.GetRootExceptionMessage(ex);

            if (ex is ServiceException e)
            {
                switch (e.ExceptionType)
                {
                    case ExceptionType.Forbidden:
                        statusCode = HttpStatusCode.Forbidden;
                        break;
                    case ExceptionType.NotFound:
                        statusCode = HttpStatusCode.NotFound;
                        break;
                    default:
                        statusCode = HttpStatusCode.BadRequest;
                        break;
                }
            }

            else if (ex is SecurityTokenException s)
            {
                return Unauthorized(s.Message);
            }

            if (statusCode == HttpStatusCode.NotFound)
            {
                return NotFound(message);
            }
            if (statusCode == HttpStatusCode.Forbidden)
            {
                return Forbid();
            }

            return BadRequest(message);
        }

        protected async Task<bool> AuthenticateStudent(IStudentService studentService, Guid studentId)
        {
            if (User.IsType(UserTypes.Student))
            {
                var user = await UserService.GetUserByPrincipal(User);

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