using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data.Settings;

namespace MyPortal.Logic.Identity;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUser(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    
    public Guid GetUserId()
    {
        var principal = GetPrincipal();

        return principal.GetUserId();
    }

    public async Task<UserModel> GetUserModel(IUnitOfWork unitOfWork)
    {
        var userId = GetUserId();

        var user = await unitOfWork.Users.GetById(userId);

        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        return new UserModel(user);
    }

    public ClaimsPrincipal GetPrincipal()
    {
        return _contextAccessor.HttpContext?.User;
    }
}