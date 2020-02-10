using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyPortal.Database.Helpers
{
    internal static class EntityHelper
    {
        internal static string GetAllColumns(Type t, string tblAlias)
        {
            if (t == null) return string.Empty;
            PropertyInfo[] props = t.GetProperties().Where(x => !x.GetGetMethod().IsVirtual).ToArray();
            string properties = "";

            foreach (PropertyInfo prp in props)
            {
                if (properties != "")
                {
                    properties = $"{properties},[{tblAlias}].[{prp.Name}]";
                }
                else
                {
                    properties = $"[{tblAlias}].[{prp.Name}]";
                }
            }

            return properties;
        }

        internal static string GetTblName(Type t, string tblAlias = null)
        {
            return $"[dbo].[{t.Name}] AS [{(!string.IsNullOrWhiteSpace(tblAlias) ? tblAlias : t.Name)}]";
        }
    }
}
