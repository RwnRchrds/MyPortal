using MyPortal.Database.Models.Entity;

namespace MyPortal.Logic.Helpers
{
    internal class CacheKeys
    {
        public const string LocalSchoolName = "LocalSchoolName";
    }

    internal class CacheHelper
    {
        internal static readonly ThreadSafeMemoryCache<string> StringCache = new ThreadSafeMemoryCache<string>();
        internal static readonly ThreadSafeMemoryCache<Role> RoleCache = new ThreadSafeMemoryCache<Role>();
    }
}