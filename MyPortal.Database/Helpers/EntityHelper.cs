using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;

namespace MyPortal.Database.Helpers
{
    internal static class EntityHelper
    {
        internal static string[] GetPropertyNames(Type t, string alias = null)
        {
            var propNames = new List<string>();

            string tblAlias;
            GetTableName(t, out tblAlias, alias);
            var props = GetProperties(t);

            foreach (var prop in props)
            {
                propNames.Add($"{tblAlias}.{prop.Name}");
            }

            return propNames.ToArray();
        }

        private static IEnumerable<PropertyInfo> GetProperties(Type t)
        {
            var props = new List<PropertyInfo>();

            props.AddRange(t.GetProperties().Where(p => Attribute.IsDefined(p, typeof(ColumnAttribute))).OrderBy(p =>
                ((ColumnAttribute) Attribute.GetCustomAttribute(p, typeof(ColumnAttribute)))?.Order).ToList());

            return props;
        }

        internal static string GetTableName(Type t, out string outputAlias, string tblAlias = null, bool includeSchema = false, string schema = "dbo")
        {
            var entityTable = ((TableAttribute) t.GetCustomAttribute(typeof(TableAttribute)))?.Name ?? t.Name;

            if (!string.IsNullOrWhiteSpace(tblAlias))
            {
                outputAlias = tblAlias;

                if (includeSchema)
                {
                    return $"{schema}.{entityTable} as {tblAlias}";   
                }
                
                return $"{entityTable} as {tblAlias}";
            }

            outputAlias = entityTable;

            if (includeSchema)
            {
                return $"{schema}.{entityTable}";   
            }
            
            return $"{entityTable}";
        }

        internal static string GetTableName(Type t, string tblAlias = null, bool includeSchema = false, string schema = "dbo")
        {
            var entityTable = ((TableAttribute)t.GetCustomAttribute(typeof(TableAttribute)))?.Name ?? t.Name;

            if (!string.IsNullOrWhiteSpace(tblAlias))
            {
                if (includeSchema)
                {
                    return $"{schema}.{entityTable} as {tblAlias}";
                }

                return $"{entityTable} as {tblAlias}";
            }

            if (includeSchema)
            {
                return $"{schema}.{entityTable}";
            }

            return $"{entityTable}";
        }
    }
}
