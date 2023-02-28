using System;
using System.Threading.Tasks;
using MyPortal.Database.Enums;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Identity;

public class CustomCurrentUser : ICurrentUser
{
    private readonly Guid _userId;

    public CustomCurrentUser(Guid userId)
    {
        _userId = userId;
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
        return _userId;
    }
}