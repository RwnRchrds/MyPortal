using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Exceptions;
using Syncfusion.EJ2.Base;

namespace MyPortalCore.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase, IDisposable
    {
        protected readonly IApplicationUserService _userService;

        public BaseApiController(IApplicationUserService userService)
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

        public abstract void Dispose();
    }
}