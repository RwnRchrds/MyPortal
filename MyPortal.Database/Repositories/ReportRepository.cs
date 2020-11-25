using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ReportRepository : BaseReadRepository<Report>, IReportRepository
    {
        public ReportRepository(IDbConnection connection) : base(connection, "Report")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(SystemArea), "SystemArea");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("SystemAreas as SystemArea", "SystemArea.Id", "Report.SystemAreaId");
        }

        protected override async Task<IEnumerable<Report>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Report, SystemArea, Report>(sql.Sql, (report, area) =>
            {
                report.SystemArea = area;

                return report;
            }, sql.NamedBindings);
        }
    }
}