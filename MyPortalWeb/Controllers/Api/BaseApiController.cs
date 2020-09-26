using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Database.Constants;
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

        public BaseApiController(IUserService userService)
        {
            UserService = userService;
        }

        public virtual void Dispose()
        {
            UserService.Dispose();
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

        protected async Task<bool> AuthenticateStudentResource(IStudentService studentService, Guid studentId)
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