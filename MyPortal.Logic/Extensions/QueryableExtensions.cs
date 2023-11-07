using System.Linq;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Logic.Extensions
{
    internal static class QueryableExtensions
    {
        internal static IQueryable<T> WhereActive<T>(this IQueryable<T> collection) where T : LookupItem
        {
            return collection.Where(x => x.Active);
        }
    }
}