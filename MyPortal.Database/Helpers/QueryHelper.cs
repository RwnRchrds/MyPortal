using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Entity;
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

        public static Query JoinStudentGroups(this Query query, string studentAlias, string studentGroupMembershipAlias)
        {
            query.LeftJoin($"StudentGroupMemberships as {studentGroupMembershipAlias}", $"{studentGroupMembershipAlias}.StudentId",
                $"{studentAlias}.Id");

            return query;
        }

        public static Query ApplyName(this Query query, string nameAlias, string personIdColumn, NameFormat format = NameFormat.Default, bool usePreferredName = false, bool includeMiddleName = true)
        {
            var nameQuery = new Query("People as P");

            nameQuery.Select("P.Id as PersonId");
            
            if (format == NameFormat.FullName)
            {
                nameQuery.SelectRaw($@"CONCAT(IIF(P.Title IS NOT NULL, CONCAT(P.Title, ' '), ''),
        {(usePreferredName ? "COALESCE(P.PreferredFirstName, P.FirstName)" : "P.FirstName")},
        {(includeMiddleName ? "IIF(P.MiddleName IS NOT NULL, CONCAT(' ', P.MiddleName, ' '), ' ')" : "' '")},
        {(usePreferredName ? "COALESCE(P.PreferredLastName, P.LastName)" : "P.LastName")}");
            }
            else if (format == NameFormat.FullNameAbbreviated)
            {
                nameQuery.SelectRaw($@"CONCAT(IIF(P.Title IS NOT NULL, CONCAT(P.Title, ' '), ''),
        SUBSTRING({(usePreferredName ? "COALESCE(P.PreferredFirstName, P.FirstName)" : "P.FirstName")}, 1, 1),
        {(includeMiddleName ? "IIF(P.MiddleName IS NOT NULL, CONCAT(' ', SUBSTRING(P.MiddleName, 1, 1), ' '), ' ')" : " ")},
{(usePreferredName ? "COALESCE(P.PreferredLastName, P.LastName)" : "P.LastName")}");
            }
            else if (format == NameFormat.FullNameNoTitle)
            {
                nameQuery.SelectRaw(
                    $@"CONCAT({(usePreferredName ? "COALESCE(P.PreferredFirstName, P.FirstName)" : "P.FirstName")},
        {(includeMiddleName ? "IIF(P.MiddleName IS NOT NULL, CONCAT(' ', P.MiddleName, ' '), ' ')" : "' '")},
{(usePreferredName ? "COALESCE(P.PreferredLastName, P.LastName)" : "P.LastName")}");
            }
            else if (format == NameFormat.Initials)
            {
                nameQuery.SelectRaw(
                    $@"CONCAT(SUBSTRING({(usePreferredName ? "COALESCE(P.PreferredFirstName, P.FirstName)" : "P.FirstName")}, 1, 1),
        {(includeMiddleName ? "IIF(P.MiddleName IS NOT NULL, SUBSTRING(P.MiddleName, 1, 1), '')" : " ")},
        SUBSTRING({(usePreferredName ? "COALESCE(P.PreferredLastName, P.LastName)" : "P.LastName")}, 1, 1)) as [Name]");
            }
            else
            {
                nameQuery.SelectRaw($@"CONCAT({(usePreferredName ? "COALESCE(P.PreferredLastName, P.LastName)" : "P.LastName")},
        ', ', {(usePreferredName ? "COALESCE(P.PreferredFirstName, P.FirstName)" : "P.FirstName")},
        {(includeMiddleName ? "IIF(P.MiddleName IS NOT NULL, CONCAT(' ', P.MiddleName), '')" : "")}) as [Name]");
            }

            nameQuery.Where("P.Id", personIdColumn);
            
            return query.Join(nameQuery, j => j, "cross apply");
        }

        public static Query ApplyOverlappingEvents(this Query query, string diaryEventAlias, string startTimeColumn, string endTimeColumn, Guid? eventTypeFilter)
        {
            var eventQuery = new Query("DiaryEvents DE");

            eventQuery.SelectAllColumns(typeof(DiaryEvent), "DE");

            eventQuery.Where(x =>
                x.Where(y =>
                        y.Where(z =>
                                z.Where("DE.EndTime", ">=", startTimeColumn).Where("DE.StartTime", "<=", endTimeColumn))
                            .OrWhere(z =>
                                z.Where(startTimeColumn, ">=", "DE.StartTime")
                                    .Where(endTimeColumn, "<=", "DE.EndTime")))
                    .Where(y => y.Where(eventTypeFilter == null).OrWhere("DE.EventTypeId", eventTypeFilter)));

            return query.Join(eventQuery, j => j, "cross apply");
        }

        public static Query WhereStudentGroup(this Query query, string studentGroupMembershipAlias,
            Guid studentGroupId, DateTime runAsDate)
        {
            query.Where($"{studentGroupMembershipAlias}.StudentGroupId", studentGroupId);
            query.Where($"{studentGroupMembershipAlias}.StartDate", "<=", runAsDate);
            query.Where(q =>
                q.WhereNull($"{studentGroupMembershipAlias}.EndDate")
                    .OrWhere($"{studentGroupMembershipAlias}.EndDate", ">=", runAsDate));

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

        public static Query ApplyPaging(this Query query, PageFilter filter)
        {
            query.Skip(filter.Skip);
            query.Take(filter.Take);

            return query;
        }
    }
}
