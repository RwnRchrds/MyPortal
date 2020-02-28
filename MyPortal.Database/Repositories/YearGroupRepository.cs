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
{EntityHelper.GetAllColumns(typeof(StaffMember))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[StaffMember].[Id]", "[YearGroup].[HeadId]")}";
        }

        protected override async Task<IEnumerable<YearGroup>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<YearGroup, StaffMember, YearGroup>(sql, (yearGroup, head) =>
            {
                yearGroup.HeadOfYear = head;

                return yearGroup;
            }, param);
        }

        public async Task Update(YearGroup entity)
        {
            var yearGroup = await Context.YearGroups.FindAsync(entity.Id);

            yearGroup.Name = entity.Name;
            yearGroup.HeadId = entity.HeadId;
            yearGroup.KeyStage = entity.KeyStage;
        }
    }
}