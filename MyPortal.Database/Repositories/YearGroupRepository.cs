﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class YearGroupRepository : BaseReadWriteRepository<YearGroup>, IYearGroupRepository
    {
        public YearGroupRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(StaffMember))},
{EntityHelper.GetPropertyNames(typeof(Person))},
{EntityHelper.GetPropertyNames(typeof(CurriculumYearGroup))}";

            JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[StaffMember].[Id]", "[YearGroup].[HeadId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[StaffMember].[PersonId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[CurriculumYearGroup]", "[CurriculumYearGroup].[Id]", "[YearGroup].[Id]")}";
        }

        protected override async Task<IEnumerable<YearGroup>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<YearGroup, StaffMember, Person, CurriculumYearGroup, YearGroup>(sql, (yearGroup, head, person, curriculumGroup) =>
            {
                yearGroup.HeadOfYear = head;
                yearGroup.HeadOfYear.Person = person;

                yearGroup.CurriculumYearGroup = curriculumGroup;

                return yearGroup;
            }, param);
        }
    }
}