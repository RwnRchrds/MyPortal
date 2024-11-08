using System;
using System.Threading.Tasks;
using MyPortal.Database.Enums;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Identity;

public abstract class BaseSessionUser : ISessionUser
{
    public async Task<IUnitOfWork> GetConnection()
    {
        var userId = GetUserId();

        if (userId.HasValue)
        {
            return await DataConnectionFactory.CreateUnitOfWork(userId.Value);
        }

        // Anonymous database access
        return await DataConnectionFactory.CreateUnitOfWork(Guid.Empty);
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
    
    public async Task<bool> HasPermission(IUserService userService,
        params PermissionValue[] permissionValues)
    {
        var userId = GetUserId();

        if (userId.HasValue)
        {
            return await PermissionHelper.UserHasPermission(userId.Value, userService, PermissionRequirement.RequireAny, permissionValues);
        }

        return false;
    }

    public abstract bool IsType(int userType);

    public abstract Guid? GetUserId();
}