using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Caching;

namespace MyPortal.Logic.Interfaces
{
    public interface IIdentityServiceCollection : IDisposable
    {
        UserManager<User> UserManager { get; }
        RoleManager<Role> RoleManager { get; }
        SignInManager<User> SignInManager { get; }
        IRolePermissionsCache RolePermissionsCache { get; }
    }
}
