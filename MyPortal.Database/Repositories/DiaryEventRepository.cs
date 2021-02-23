using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Constants;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DiaryEventRepository : BaseReadWriteRepository<DiaryEvent>, IDiaryEventRepository
    {
        public DiaryEventRepository(ApplicationDbContext context) : base(context, "DiaryEvent")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(DiaryEventType), "DiaryEventType");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("DiaryEventTypes as DiaryEventType", "DiaryEventType.Id", "DiaryEvent.EventTypeId");
        }

        protected override async Task<IEnumerable<DiaryEvent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<DiaryEvent, DiaryEventType, DiaryEvent>(sql.Sql, (diaryEvent, type) =>
            {
                diaryEvent.EventType = type;
                return diaryEvent;
            }, sql.NamedBindings);
        }

        public async Task<IEnumerable<DiaryEvent>> GetByDateRange(DateTime firstDate, DateTime lastDate, bool includePrivateEvents = false)
        {
            var query = GenerateQuery();

            query.WhereDate("DiaryEvent.StartTime", ">=", firstDate.Date);
            query.WhereDate("DiaryEvent.EndTime", "<", lastDate.Date.AddDays(1));

            if (!includePrivateEvents)
            {
                query.WhereTrue("DiaryEvent.IsPublic");
            }

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<DiaryEvent>> GetByPerson(DateTime firstDate, DateTime lastDate, Guid personId, bool includeDeclined = false)
        {
            var query = GenerateQuery();

            query.LeftJoin("DiaryEventAttendees as A", "A.EventId", "DiaryEvent.Id");
            
            query.WhereDate("DiaryEvent.StartTime", ">=", firstDate.Date);
            query.WhereDate("DiaryEvent.EndTime", "<", lastDate.Date.AddDays(1));

            query.Where("A.PersonId", personId);

            if (!includeDeclined)
            {
                query.Where(q => q.Where("A.ResponseId", "<>", AttendeeResponses.Declined).WhereFalse("A.Required"));
            }

            return await ExecuteQuery(query);
        }
    }
}
