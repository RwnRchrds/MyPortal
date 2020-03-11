using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class YearGroupRepository : BaseReadWriteRepository<YearGroup>, IYearGroupRepository
    {
        public YearGroupRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(StaffMember))},
{EntityHelper.GetAllColumns(typeof(Person))},
{EntityHelper.GetAllColumns(typeof(CurriculumYearGroup))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[StaffMember].[Id]", "[YearGroup].[HeadId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[StaffMember].[PersonId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[CurriculumYearGroup]", "[CurriculumYearGroup].[Id]", "[YearGroup].[Id]")}";
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

        public async Task Update(YearGroup entity)
        {
            var yearGroup = await Context.YearGroups.FindAsync(entity.Id);

            yearGroup.Name = entity.Name;
            yearGroup.HeadId = entity.HeadId;
            yearGroup.CurriculumYearGroupId = entity.CurriculumYearGroupId;
        }
    }
}