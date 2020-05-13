using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        protected readonly IMapper _dTMapper;
        protected readonly IApplicationUserService _userService;

        public BaseApiController(IApplicationUserService userService)
        {
            _dTMapper = MappingHelper.GetDataGridConfig();
            _userService = userService;
        }

        protected async Task<IActionResult> Process(Func<Task<IActionResult>> method)
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

        private IActionResult HandleException(Exception ex)
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

            return BadRequest(ex.Message);
        }

        public abstract void Dispose();
    }
}