using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DiaryEventTemplateRepository : BaseReadWriteRepository<DiaryEventTemplate>, IDiaryEventTemplateRepository
    {
        public DiaryEventTemplateRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(DiaryEventType), "EventType");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.DiaryEventType as EventType", "EventType.Id", "DiaryEventTemplate.EventTypeId");
        }

        protected override async Task<IEnumerable<DiaryEventTemplate>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<DiaryEventTemplate, DiaryEventType, DiaryEventTemplate>(sql.Sql,
                (template, eventType) =>
                {
                    template.DiaryEventType = eventType;

                    return template;
                }, sql.Bindings);
        }
    }
}