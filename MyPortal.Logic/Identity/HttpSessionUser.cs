using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Identity;

public class HttpSessionUser : BaseSessionUser, ISessionUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpSessionUser(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public override bool IsType(int userType)
    {
        var principal = GetPrincipal();

        return principal.IsType(userType);
    }

    public override Guid? GetUserId()
    {
        var principal = GetPrincipal();

        return principal.GetUserId();
    }

    public ClaimsPrincipal GetPrincipal()
    {
        return _contextAccessor.HttpContext?.User;
    }
}