using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Helpers
{
    internal class CacheKeys
    {
        public const string LocalSchoolName = "LocalSchoolName";
    }
    internal class CacheHelper
    {
        internal static readonly ThreadSafeMemoryCache<string> StringCache = new ThreadSafeMemoryCache<string>();
    }
}
