using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using MyPortal.Database.Models;

namespace MyPortal.Database.Helpers
{
    internal static class EntityHelper
    {
        internal static string[] GetPropertyNames(Type t, string alias = null)
        {
            var propNames = new List<string>();

            if (t == typeof(Role))
            {
                return GetRolePropertyNames(alias);
            }

            if (t == typeof(User))
            {
                return GetUserPropertyNames(alias);
            }

            string tblAlias;
            GetTableName(t, out tblAlias, alias);
            var props = GetProperties(t);

            foreach (var prop in props)
            {
                propNames.Add($"{tblAlias}.{prop.Name}");
            }

            return propNames.ToArray();
        }

        internal static string[] GetRolePropertyNames(string alias = null)
        {
            string tblAlias = string.IsNullOrWhiteSpace(alias) ? "AspNetRoles" : alias;

            var propNames = new List<string>
            {
                $"{tblAlias}.Id",
                $"{tblAlias}.Name",
                $"{tblAlias}.NormalizedName",
                $"{tblAlias}.Description",
                $"{tblAlias}.ConcurrencyStamp",
                $"{tblAlias}.System"
            };

            return propNames.ToArray();
        }

        internal static string[] GetUserPropertyNames(string alias = null)
        {
            string tblAlias = string.IsNullOrWhiteSpace(alias) ? "AspNetUsers" : alias;

            var propNames = new List<string>
            {
                $"{tblAlias}.Id",
                $"{tblAlias}.UserName",
                $"{tblAlias}.NormalizedUserName",
                $"{tblAlias}.Email",
                $"{tblAlias}.NormalizedEmail",
                $"{tblAlias}.EmailConfirmed",
                $"{tblAlias}.PasswordHash",
                $"{tblAlias}.SecurityStamp",
                $"{tblAlias}.ConcurrencyStamp",
                $"{tblAlias}.PhoneNumber",
                $"{tblAlias}.PhoneNumberConfirmed",
                $"{tblAlias}.TwoFactorEnabled",
                $"{tblAlias}.LockoutEnd",
                $"{tblAlias}.LockoutEnabled",
                $"{tblAlias}.AccessFailedCount",
                $"{tblAlias}.CreatedDate",
                $"{tblAlias}.PersonId",
                $"{tblAlias}.UserType",
                $"{tblAlias}.Enabled"
            };

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
