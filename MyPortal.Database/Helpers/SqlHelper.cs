using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Helpers
{
    internal enum JoinType
    {
        InnerJoin,
        LeftJoin,
        RightJoin,
        FullJoin
    }

    internal static class SqlHelper
    {
        internal static string Join(JoinType joinType, string tblName, string @on, string @equals, string tblAlias = null)
        {
            string join;

            switch (joinType)
            {
                case JoinType.InnerJoin:
                    join = "INNER JOIN";
                    break;
                case JoinType.LeftJoin:
                    join = "LEFT JOIN";
                    break;
                case JoinType.RightJoin:
                    join = "RIGHT JOIN";
                    break;
                default:
                    join = "FULL JOIN";
                    break;
            }

            var alias = !string.IsNullOrWhiteSpace(tblAlias) ? $"[{tblAlias}]" : tblName.Replace("[dbo].", "");

            return $"{join} {tblName} AS {alias} ON {@on} = {@equals}";
        }

        internal static string Where(string sql, string condition)
        {
            var clause = sql.Contains(" WHERE ") ? "AND" : "WHERE";

            return $@"{sql} {clause} {condition}";
        }

        internal static string BuildQuery(string sql, params Func<string>[] appends)
        {
            foreach (var append in appends)
            {
                sql = append.Invoke();
            }

            return sql;
        }

        internal static string ParamStartsWith(string param)
        {
            return $"{param}%";
        }

        internal static string ParamEndsWith(string param)
        {
            return $"%{param}";
        }

        internal static string ParamContains(string param)
        {
            return $"%{param}%";
        }
    }
}
