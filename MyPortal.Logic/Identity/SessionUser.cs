using System;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Identity;

public class SessionUser : ISessionUser
{
    private readonly Guid _userId;

    public static ISessionUser System => new SessionUser(Users.System);

    public SessionUser(Guid userId)
    {
        _userId = userId;
    }

    public async Task<IUnitOfWork> GetConnection()
    {
        return await DataConnectionFactory.CreateUnitOfWork(_userId);
    }

    public async Task<bool> HasPermission(IUserService userService, PermissionRequirement requirement,
        params PermissionValue[] permissionValues)
    {
        var userId = GetUserId();

        if (userId != null)
        {
            return await PermissionHelper.UserHasPermission(userId.Value, userService, requirement, permissionValues);
        }

        return false;
    }

    public Guid? GetUserId()
    {
        return _userId;
    }
}