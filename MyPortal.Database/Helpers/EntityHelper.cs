using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyPortal.Database.Helpers
{
    internal static class EntityHelper
    {
        internal static string GetAllColumns(Type t, string tblAlias = null)
        {
            if (t == null) return string.Empty;
            PropertyInfo[] props = t.GetProperties().Where(x => !x.GetGetMethod().IsVirtual).ToArray();
            string properties = "";

            if (string.IsNullOrWhiteSpace(tblAlias))
            {
                tblAlias = t.Name;
            }

            var first = true;

            foreach (var prp in props)
            {
                if (first)
                {
                    properties = $"[{tblAlias}].[{prp.Name}]";
                    first = false;
                }
                else
                {
                    properties = $"{properties},[{tblAlias}].[{prp.Name}]";
                }
            }

            return properties;
        }

        internal static string GetTblName(Type t, string tblAlias = null, string schema = "dbo")
        {
            return $"[{schema}].[{t.Name}] AS [{(!string.IsNullOrWhiteSpace(tblAlias) ? tblAlias : t.Name)}]";
        }
    }
}
