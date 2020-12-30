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

        public async Task<Guid[]> GetPermissions(params Guid[] roleIds)
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
                    permissionIds.AddRange((await _rolePermissionRepository.GetByRole(roleId)).Select(x => x.PermissionId));
                    _cache.Set(key, permissionIds, TimeSpan.FromHours(1));
                }
            }

            return permissionIds.ToArray();
        }

        public void Purge(params Guid[] roleIds)
        {
            foreach (var roleId in roleIds)
            {
                var key = new KeyValuePair<string, Guid>(CacheTypes.RolePermission, roleId);

                _cache.Remove(key);
            }
        }
    }
}
