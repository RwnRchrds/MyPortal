using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyPortal.Database.Enums;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Identity;

public class HttpSessionUser : ISessionUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpSessionUser(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task<IUnitOfWork> GetConnection()
    {
        var userId = GetUserId();

        if (userId.HasValue)
        {
            return await DataConnectionFactory.CreateUnitOfWork(userId.Value);
        }

        return null;
    }

    public async Task<bool> HasPermission(IUserService userService, PermissionRequirement requirement,
        params PermissionValue[] permissionValues)
    {
        var userId = GetUserId();

        if (userId.HasValue)
        {
            return await PermissionHelper.UserHasPermission(userId.Value, userService, requirement, permissionValues);
        }

        return false;
    }

    public Guid? GetUserId()
    {
        var principal = GetPrincipal();

        return principal.GetUserId();
    }

    public ClaimsPrincipal GetPrincipal()
    {
        return _contextAccessor.HttpContext?.User;
    }
}