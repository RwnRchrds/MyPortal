using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class CommunicationLogRepository : BaseReadWriteRepository<CommunicationLog>, ICommunicationLogRepository
    {
        public CommunicationLogRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
      
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(CommunicationType));
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("CommunicationType", "CommunicationType.Id", "CommnicationLog.CommunicationTypeId");
        }

        protected override async Task<IEnumerable<CommunicationLog>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<CommunicationLog, CommunicationType, CommunicationLog>(sql.Sql, (log, type) =>
            {
                log.Type = type;

                return log;
            }, sql.NamedBindings);
        }
    }
}