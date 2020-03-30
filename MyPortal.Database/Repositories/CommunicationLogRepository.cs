using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class CommunicationLogRepository : BaseReadWriteRepository<CommunicationLog>, ICommunicationLogRepository
    {
        public CommunicationLogRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(CommunicationType))}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[CommunicationType]", "[CommunicationType].[Id]", "[CommunicationLog].[CommunicationTypeId]")}";
        }

        protected override async Task<IEnumerable<CommunicationLog>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<CommunicationLog, CommunicationType, CommunicationLog>(sql, (log, type) =>
            {
                log.Type = type;

                return log;
            }, param);
        }
    }
}