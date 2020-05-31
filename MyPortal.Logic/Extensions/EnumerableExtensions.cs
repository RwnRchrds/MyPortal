using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Database.BaseTypes;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Extensions
{
    public static class EnumerableExtensions
    {
        public static Lookup ToLookup(this IEnumerable<LookupItem> itemList)
        {
            var lookup = new Lookup();

            foreach (var lookupItem in itemList)
            {
                lookup.Add(lookupItem.Description, lookupItem.Id);
            }

            return lookup;
        }
    }
}
