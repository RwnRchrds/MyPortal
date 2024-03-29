﻿using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Database.Enums;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Response;

namespace MyPortalWeb.Controllers.BaseControllers
{
    [Authorize]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IUserService UserService;

        protected BaseApiController(IUserService userService)
        {
            UserService = userService;
        }

        protected async Task<UserModel> GetLoggedInUser()
        {
            return await UserService.GetUserByPrincipal(User);
        }

        protected string PermissionMessage => "You do not have permission to access this resource.";

        protected async Task<bool> UserHasPermission(PermissionRequirement requirement,
            params PermissionValue[] permissionValues)
        {
            return await User.HasPermission(UserService, requirement, permissionValues);
        }

        protected async Task<bool> UserHasPermission(params PermissionValue[] permissionValues)
        {
            return await User.HasPermission(UserService, PermissionRequirement.RequireAny, permissionValues);
        }

        protected IActionResult HandleException(Exception ex)
        {
            HttpStatusCode statusCode;

            var message = ex.GetBaseException().Message;

            switch (ex)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case SecurityTokenException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case PermissionException:
                    statusCode = HttpStatusCode.Forbidden;
                    break;
                case LogicException:
                case InvalidDataException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case NotImplementedException:
                    statusCode = HttpStatusCode.NotImplemented;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            return Error((int)statusCode, message);
        }

        protected IActionResult PermissionError()
        {
            return Error(HttpStatusCode.Forbidden, PermissionMessage);
        }

        protected IActionResult Error(int statusCode, string errorMessage)
        {
            var error = new ErrorResponseModel(errorMessage);

            return StatusCode(statusCode, error);
        }

        protected IActionResult Error(HttpStatusCode statusCode, string errorMessage)
        {
            return Error((int)statusCode, errorMessage);
        }
    }
}