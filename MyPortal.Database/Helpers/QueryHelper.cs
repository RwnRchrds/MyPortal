using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Constants;
using MyPortal.Database.Models.Filters;
using SqlKata;

namespace MyPortal.Database.Helpers
{
    internal static class QueryHelper
    {
        public static Query SelectAllColumns(this Query query, Type type, string alias = null)
        {
            var columnNames = EntityHelper.GetPropertyNames(type, alias);

            query.Select(columnNames);

            return query;
        }

        public static Query GroupByEntityColumns(this Query query, Type type, string alias = null)
        {
            var columnNames = EntityHelper.GetPropertyNames(type, alias);

            query.GroupBy(columnNames);

            return query;
        }

        public static Query JoinStudentGroups(this Query query, string studentAlias, string studentGroupAlias = "SGM")
        {
            query.LeftJoin($"StudentGroupMemberships as {studentGroupAlias}", $"{studentGroupAlias}.StudentId",
                $"{studentAlias}.Id");

            return query;
        }

        public static Query WhereContainsWord(this Query query, string column, string searchText)
        {
            return query.Where(q =>
                q.WhereLike(column, $"% {searchText} %").OrWhereLike(column, $"% {searchText}")
                    .OrWhereLike(column, $"{searchText} %").OrWhere(column, searchText));
        }
        
        public static Query OrWhereContainsWord(this Query query, string column, string searchText)
        {
            return query.OrWhere(q =>
                q.WhereLike(column, $"% {searchText} %").OrWhereLike(column, $"% {searchText}")
                    .OrWhereLike(column, $"{searchText} %").OrWhere(column, searchText));
        }

        public static Query Limit(this Query query, PageFilter filter)
        {
            query.Skip(filter.Skip);
            query.Take(filter.Take);

            return query;
        }
    }
}
