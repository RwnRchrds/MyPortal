﻿using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Constants;
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

        public static Query GroupByAllColumns(this Query query, Type type, string alias = null)
        {
            var columnNames = EntityHelper.GetPropertyNames(type, alias);

            query.GroupBy(columnNames);

            return query;
        }

        public static Query FilterByStudentGroup(this Query query, Guid groupTypeId, Guid groupId, string studentAlias = "S")
        {
            if (groupTypeId == StudentGroupType.YearGroup)
            {
                query.LeftJoin("YearGroups AS Y", "Y.Id", $"{studentAlias}.YearGroupId");
                query.Where("Y.Id", groupId);
            }

            if (groupTypeId == StudentGroupType.CurriculumGroup)
            {
                query.LeftJoin("CurriculumGroupMemberships AS CGM", "CGM.StudentId", $"{studentAlias}.Id");
                query.Where("CGM.GroupId", groupId);
            }

            if (groupTypeId == StudentGroupType.RegGroup)
            {
                query.LeftJoin("RegGroups AS R", "R.Id", $"{studentAlias}.RegGroupId");
                query.Where("R.Id", groupId);
            }

            return query;
        }
    }
}
