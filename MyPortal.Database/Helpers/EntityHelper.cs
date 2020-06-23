using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Helpers
{
    internal static class EntityHelper
    {
        internal static string[] GetPropertyNames(Type t, string alias = null)
        {
            var propNames = new List<string>();

            if (t == typeof(ApplicationUser))
            {
                propNames = GetUserProperties(alias);
            }
            else
            {
                var tblName = string.IsNullOrWhiteSpace(alias) ? t.Name : alias;
                var props = GetProperties(t);

                foreach (var prop in props)
                {
                    propNames.Add($"{tblName}.{prop.Name}");
                }
            }

            return propNames.ToArray();
        }

        private static List<string> GetUserProperties(string alias = null)
        {
            var tblName = string.IsNullOrWhiteSpace(alias) ? "AspNetUsers" : alias;

            var propNames = new List<string>
            {
                $"{tblName}.Id",
                $"{tblName}.UserName",
                $"{tblName}.NormalizedUserName",
                $"{tblName}.Email",
                $"{tblName}.NormalizedEmail",
                $"{tblName}.EmailConfirmed",
                $"{tblName}.PhoneNumber",
                $"{tblName}.PhoneNumberConfirmed",
                $"{tblName}.TwoFactorEnabled",
                $"{tblName}.LockoutEnd",
                $"{tblName}.AccessFailedCount",
                $"{tblName}.Enabled"
            };

            return propNames;
        }

        private static IEnumerable<PropertyInfo> GetProperties(Type t)
        {
            var props = new List<PropertyInfo>();

            props.AddRange(t.GetProperties().Where(p => Attribute.IsDefined(p, typeof(ColumnAttribute))).OrderBy(p =>
                ((ColumnAttribute) Attribute.GetCustomAttribute(p, typeof(ColumnAttribute)))?.Order).ToList());

            return props;
        }

        internal static string GetTableName(Type t, string tblAlias = null, string schema = "dbo")
        {
            var entityTable = ((TableAttribute) t.GetCustomAttribute(typeof(TableAttribute)))?.Name ?? t.Name;
            
            if (t == typeof(ApplicationUser))
            {
                entityTable = "AspNetUsers";
            }

            if (t == typeof(ApplicationRole))
            {
                entityTable = "AspNetRoles";
            }

            if (!string.IsNullOrWhiteSpace(tblAlias))
            {
                return $"{schema}.{entityTable} as {tblAlias}";
            }

            return $"{schema}.{entityTable}";
        }
    }
}
