using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Enums;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Settings;

namespace MyPortal.Logic.Interfaces;

public interface ICurrentUser
{
    Task<IUnitOfWork> GetConnection();

    Task<bool> HasPermission(IUserService userService, PermissionRequirement requirement,
        params PermissionValue[] permissionValues);
    Guid GetUserId();
}