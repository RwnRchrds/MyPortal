using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Logic.Constants;

namespace MyPortal.Logic.Caching
{
    public class RolePermissionsCache : IRolePermissionsCache
    {
        private readonly IMemoryCache _cache;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionsCache(IMemoryCache cache, IRolePermissionRepository rolePermissionRepository)
        {
            _cache = cache;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<Guid[]> GetPermissions(Guid roleId)
        {
            var key = new KeyValuePair<string, Guid>(CacheTypes.RolePermission, roleId);

            if (_cache.TryGetValue(key, out var cachedPermissions))
            {
                return cachedPermissions as Guid[];
            }

            var permissionIds = (await _rolePermissionRepository.GetByRole(roleId)).Select(x => x.PermissionId).ToArray();

            _cache.Set(key, permissionIds, TimeSpan.FromHours(1));

            return permissionIds;
        }
    }
}
