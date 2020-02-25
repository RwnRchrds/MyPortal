using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class ReportRepository : BaseReadRepository<Report>, IReportRepository
    {
        public ReportRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
            RelatedColumns = $@"{EntityHelper.GetAllColumns(typeof(SystemArea))}";

            JoinRelated = $@"{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[SystemArea]", "[SystemArea].[Id]", "[Report].[SystemAreaId]")}";
        }

        protected override async Task<IEnumerable<Report>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Report>(sql, param);
        }
    }
}