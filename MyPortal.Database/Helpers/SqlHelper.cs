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

        internal static void Where(ref string sql, string condition)
        {
            var clause = sql.Contains(" WHERE ") ? "AND" : "WHERE";

            sql = $@"{sql} {clause} {condition}";
        }

        internal static string ParamStartsWith(string param)
        {
            return $"{param}%";
        }

        internal static string ParamContains(string param)
        {
            return $"%{param}%";
        }
    }
}
