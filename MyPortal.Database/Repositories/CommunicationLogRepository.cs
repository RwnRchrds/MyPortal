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
        public CommunicationLogRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
      
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Contacts as C", "C.Id", $"{TblAlias}.ContactId");
            query.LeftJoin("CommunicationTypes as CT", "CT.Id", $"{TblAlias}.CommunicationTypeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Contact), "C");
            query.SelectAllColumns(typeof(CommunicationType), "CT");

            return query;
        }

        protected override async Task<IEnumerable<CommunicationLog>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var communicationLogs =
                await Transaction.Connection.QueryAsync<CommunicationLog, Contact, CommunicationType, CommunicationLog>(
                    sql.Sql,
                    (log, contact, type) =>
                    {
                        log.Contact = contact;
                        log.Type = type;

                        return log;
                    }, sql.NamedBindings, Transaction);

            return communicationLogs;
        }

        public async Task Update(CommunicationLog entity)
        {
            var log = await Context.CommunicationLogs.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (log == null)
            {
                throw new EntityNotFoundException("Communication log not found.");
            }

            log.Notes = entity.Notes;
            log.Date = entity.Date;
            log.Outgoing = entity.Outgoing;
        }
    }
}