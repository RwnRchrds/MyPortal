using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Models.Exceptions;
using Syncfusion.EJ2.Base;

namespace MyPortalCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult HandleException(Exception ex)
        {
            var statusCode = HttpStatusCode.BadRequest;

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
                return NotFound(ex.Message);
            }
            if (statusCode == HttpStatusCode.Forbidden)
            {
                return Forbid(ex.Message);
            }
            else
            {
                return BadRequest(ex.Message);
            }
        }
    }
}