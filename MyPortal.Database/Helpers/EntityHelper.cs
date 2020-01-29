using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyPortal.Database.Helpers
{
    public class EntityHelper
    {
        public static string GetAllColumns(Type t, string tblAlias)
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
    }
}
