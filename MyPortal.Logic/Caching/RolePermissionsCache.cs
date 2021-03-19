using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MyPortal.Database;
using MyPortal.Database.Models;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Caching
{
    public class RolePermissionsCache : IRolePermissionsCache
    {
        private readonly IMemoryCache _cache;

        public RolePermissionsCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<Guid[]> GetPermissions(params Guid[] roleIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var permissionIds = new List<Guid>();

                foreach (var roleId in roleIds)
                {
                    var key = new KeyValuePair<string, Guid>(CacheTypes.RolePermission, roleId);

                    if (_cache.TryGetValue(key, out var cachedPermissions))
                    {
                        permissionIds.AddRange(cachedPermissions as IEnumerable<Guid> ?? Array.Empty<Guid>());
                    }
                    else
                    {
                        permissionIds.AddRange((await unitOfWork.RolePermissions.GetByRole(roleId)).Select(x => x.PermissionId));
                        _cache.Set(key, permissionIds, TimeSpan.FromHours(1));
                    }
                }

                return permissionIds.ToArray();
            }
        }

        public void Purge(params Guid[] roleIds)
        {
            foreach (var roleId in roleIds)
            {
                var key = new KeyValuePair<string, Guid>(CacheTypes.RolePermission, roleId);

                if (_cache.TryGetValue(key, out _))
                {
                    _cache.Remove(key);
                }
            }
        }
    }
}
