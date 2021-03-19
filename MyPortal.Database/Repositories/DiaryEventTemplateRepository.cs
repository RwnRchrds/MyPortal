using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DiaryEventTemplateRepository : BaseReadWriteRepository<DiaryEventTemplate>, IDiaryEventTemplateRepository
    {
        public DiaryEventTemplateRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "DiaryEventTemplate")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(DiaryEventType), "EventType");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("DiaryEventTypes as EventType", "EventType.Id", "DiaryEventTemplate.EventTypeId");
        }

        protected override async Task<IEnumerable<DiaryEventTemplate>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<DiaryEventTemplate, DiaryEventType, DiaryEventTemplate>(sql.Sql,
                (template, eventType) =>
                {
                    template.DiaryEventType = eventType;

                    return template;
                }, sql.NamedBindings, Transaction);
        }
    }
}