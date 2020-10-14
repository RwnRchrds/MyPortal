using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Exceptions;

namespace MyPortalCore.Controllers.Api
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase, IDisposable
    {
        protected readonly IUserService _userService;
        protected readonly IAcademicYearService _academicYearService;

        public BaseApiController(IUserService userService, IAcademicYearService academicYearService)
        {
            _userService = userService;
            _academicYearService = academicYearService;
        }

        protected async Task<Guid> GetCurrentAcademicYearId()
        {
            var currentYear = await _academicYearService.GetCurrent();

            return currentYear.Id;
        }

        protected async Task<IActionResult> ProcessAsync(Func<Task<IActionResult>> method, params Guid[] permissionsRequired)
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