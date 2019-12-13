using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Data.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query,
                    (current, property) => current.Include(property));
            }

            return query;
        }
    }
}
