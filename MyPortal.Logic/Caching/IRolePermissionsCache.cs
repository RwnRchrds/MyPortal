using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace MyPortal.Logic.Caching
{
    public interface IRolePermissionsCache
    {
        Task<Guid[]> GetPermissions(params Guid[] roleIds);
        void Purge(params Guid[] roleIds);
    }
}
