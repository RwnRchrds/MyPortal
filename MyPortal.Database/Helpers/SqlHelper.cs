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

    internal enum WhereType
    {
        Equals,
        LessThan,
        GreaterThan,
        Like
    }

    internal static class SqlHelper
    {
        internal static string Join(JoinType joinType, string tblName, string leftColumn, string rightColumn, string tblAlias = null)
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

            var alias = !string.IsNullOrWhiteSpace(tblAlias) ? tblAlias : tblName.Replace("[dbo].", "");

            return $"{join} {tblName} AS [{alias}] ON {leftColumn} = {rightColumn}";
        }

        internal static string Where(WhereType whereType, string sql, string columnName, string paramName)
        {
            var clause = sql.Contains(" WHERE ") ? "AND" : "WHERE";

            string op;

            switch (whereType)
            {
                case WhereType.LessThan:
                    op = "<";
                    break;
                case WhereType.GreaterThan:
                    op = ">";
                    break;
                case WhereType.Like:
                    op = "LIKE";
                    break;
                default:
                    op = "=";
                    break;
            }

            return $"{sql} {clause} {columnName} {op} {paramName}";
        }
    }
}
