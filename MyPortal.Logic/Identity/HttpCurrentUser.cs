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
using MyPortal.Logic.Services;

namespace MyPortal.Logic.Identity;

public class HttpCurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpCurrentUser(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task<IUnitOfWork> GetConnection()
    {
        return await DataConnectionFactory.CreateUnitOfWork();
    }

    public async Task<bool> HasPermission(IUserService userService, PermissionRequirement requirement, params PermissionValue[] permissionValues)
    {
        return await PermissionHelper.UserHasPermission(GetUserId(), userService, requirement, permissionValues);
    }

    public Guid GetUserId()
    {
        var principal = GetPrincipal();

        return principal.GetUserId();
    }

    public ClaimsPrincipal GetPrincipal()
    {
        return _contextAccessor.HttpContext?.User;
    }
}