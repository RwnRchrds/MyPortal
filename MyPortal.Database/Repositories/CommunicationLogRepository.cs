using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class CommunicationLogRepository : BaseReadWriteRepository<CommunicationLog>, ICommunicationLogRepository
    {
        public CommunicationLogRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
      
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(CommunicationType));

            query = JoinRelated(query);

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("dbo.CommunicationType", "CommunicationType.Id", "CommnicationLog.CommunicationTypeId");

            return query;
        }

        protected override async Task<IEnumerable<CommunicationLog>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<CommunicationLog, CommunicationType, CommunicationLog>(sql.Sql, (log, type) =>
            {
                log.Type = type;

                return log;
            }, sql.Bindings);
        }
    }
}