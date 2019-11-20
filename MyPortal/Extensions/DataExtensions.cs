using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MyPortal.Models.Misc;
using Syncfusion.EJ2.Base;
using Syncfusion.EJ2.Linq;

namespace MyPortal.Extensions
{
    public static class DataExtensions
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

        public static ApiResponse<T> PerformDataOperations<T>(this IEnumerable<T> dataSource, DataManagerRequest dm)
        {
            DataOperations operation = new DataOperations();

            if (dm.Search != null && dm.Search.Count > 0)
            {
                dataSource = operation.PerformSearching(dataSource, dm.Search);
            }

            if (dm.Sorted != null && dm.Sorted.Count > 0)
            {
                dataSource = operation.PerformSorting(dataSource, dm.Sorted);
            }

            if (dm.Where != null && dm.Where.Count > 0)
            {
                dataSource = operation.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator);
            }

            var count = dataSource.Count();

            if (dm.Skip != 0)
            {
                dataSource = operation.PerformSkip(dataSource, dm.Skip);
            }

            if (dm.Take != 0)
            {
                dataSource = operation.PerformTake(dataSource, dm.Take);
            }

            return new ApiResponse<T> { Count = count, Items = dataSource };
        }
    }
}