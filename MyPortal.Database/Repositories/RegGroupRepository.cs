using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class RegGroupRepository : BaseReadWriteRepository<RegGroup>, IRegGroupRepository
    {
        public RegGroupRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(StaffMember))},
{EntityHelper.GetAllColumns(typeof(YearGroup))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[StaffMember].[Id]", "[RegGroup].[TutorId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[YearGroup]", "[YearGroup].[Id]", "[RegGroup].[YearGroupId]")}";
        }

        protected override async Task<IEnumerable<RegGroup>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<RegGroup, StaffMember, YearGroup, RegGroup>(sql, (reg, tutor, year) =>
            {
                reg.Tutor = tutor;
                reg.YearGroup = year;

                return reg;
            }, param);
        }
    }
}