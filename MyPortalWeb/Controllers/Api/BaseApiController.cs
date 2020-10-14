using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Database.Constants;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;

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
            HttpStatusCode statusCode;

            var message = ExceptionHelper.GetRootExceptionMessage(ex);

            switch (ex)
            {
                case NotFoundException n:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case SecurityTokenException s:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case UnauthorisedException u:
                    statusCode = HttpStatusCode.Forbidden;
                    break;
                default:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
            }

            switch (statusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound(message);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(message);
                case HttpStatusCode.Forbidden:
                    return Forbid(message);
                default:
                    return BadRequest(message);
            }
        }
    }
}