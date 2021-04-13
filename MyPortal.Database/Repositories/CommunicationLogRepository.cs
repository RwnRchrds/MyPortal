using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CommunicationLogRepository : BaseReadWriteRepository<CommunicationLog>, ICommunicationLogRepository
    {
        public CommunicationLogRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "CommunicationLog")
        {
      
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(CommunicationType), "CommunicationType");
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("CommunicationTypes as CommunicationType", "CommunicationType.Id", "CommnicationLog.CommunicationTypeId");
        }

        protected override async Task<IEnumerable<CommunicationLog>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<CommunicationLog, CommunicationType, CommunicationLog>(sql.Sql, (log, type) =>
            {
                log.Type = type;

                return log;
            }, sql.NamedBindings, Transaction);
        }

        public async Task Update(CommunicationLog entity)
        {
            var log = await Context.CommunicationLogs.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (log == null)
            {
                throw new EntityNotFoundException("Communication log not found.");
            }

            log.Note = entity.Note;
            log.Date = entity.Date;
            log.Outgoing = entity.Outgoing;
        }
    }
}