using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DiaryEventRepository : BaseReadWriteRepository<DiaryEvent>, IDiaryEventRepository
    {
        public DiaryEventRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {

        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(DiaryEventType));

            query = JoinRelated(query);

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("dbo.DiaryEventType", "DiaryEventType.Id", "DiaryEvent.EventTypeId");

            return query;
        }

        protected override async Task<IEnumerable<DiaryEvent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<DiaryEvent, DiaryEventType, DiaryEvent>(sql.Sql, (diaryEvent, type) =>
            {
                diaryEvent.EventType = type;
                return diaryEvent;
            }, sql.Bindings);
        }
    }
}
