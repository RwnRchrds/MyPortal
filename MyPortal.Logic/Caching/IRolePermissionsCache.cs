using System;
using System.Threading.Tasks;

namespace MyPortal.Logic.Caching
{
    public interface IRolePermissionsCache
    {
        Task<Guid[]> GetPermissions(params Guid[] roleIds);
        void Purge(params Guid[] roleIds);
    }
}
