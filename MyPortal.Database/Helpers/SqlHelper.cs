using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Helpers
{
    internal enum JoinType
    {
        InnerJoin,
        LeftJoin
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
                default:
                    join = "LEFT JOIN";
                    break;
            }

            var alias = !string.IsNullOrWhiteSpace(tblAlias) ? tblAlias : tblName.Replace("[dbo].", "");

            return $"{join} {tblName} AS {alias} ON {leftColumn} = {rightColumn}";
        }
    }
}
