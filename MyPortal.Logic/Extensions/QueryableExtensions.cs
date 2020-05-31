using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Logic.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Active<T>(this IQueryable<T> collection) where T : LookupItem
        {
            return collection.Where(x => x.Active);
        }
    }
}
