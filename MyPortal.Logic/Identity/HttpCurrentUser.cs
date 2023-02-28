using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Identity;

public class HttpCurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpCurrentUser(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
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