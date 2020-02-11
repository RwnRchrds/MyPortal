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

            var props = GetProperties(t);

            string columns = "";

            if (string.IsNullOrWhiteSpace(tblAlias))
            {
                tblAlias = t.Name;
            }

            var first = true;

            foreach (var prp in props)
            {
                if (first)
                {
                    columns = $"[{tblAlias}].[{prp.Name}]";
                    first = false;
                }
                else
                {
                    columns = $"{columns},[{tblAlias}].[{prp.Name}]";
                }
            }

            return columns;
        }

        internal static string GetUserColumns(string tblAlias = null)
        {
            if (string.IsNullOrWhiteSpace(tblAlias))
            {
                tblAlias = "AspNetUsers";
            }

            return
                $@"
[{tblAlias}].[Id],[{tblAlias}].[UserName],[{tblAlias}].[NormalizedUserName],[{tblAlias}].[Email],[{tblAlias}].[NormalizedEmail],
[{tblAlias}].[EmailConfirmed],[{tblAlias}].[PhoneNumber],[{tblAlias}].[PhoneNumberConfirmed],[{tblAlias}].[TwoFactorEnabled],[{tblAlias}].[LockoutEnd],
[{tblAlias}].[AccessFailedCount],[{tblAlias}].[Enabled]";
        }

        internal static IEnumerable<PropertyInfo> GetProperties(Type t)
        {
            var props = t.GetProperties().Where(x => !x.GetGetMethod().IsVirtual).ToList();

            return props;
        }

        internal static string GetTblName(Type t, string tblAlias = null, string schema = "dbo")
        {
            return $"[{schema}].[{t.Name}] AS [{(!string.IsNullOrWhiteSpace(tblAlias) ? tblAlias : t.Name)}]";
        }
    }
}
