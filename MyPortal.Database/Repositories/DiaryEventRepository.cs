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

namespace MyPortal.Database.Repositories
{
    public class DiaryEventRepository : BaseReadWriteRepository<DiaryEvent>, IDiaryEventRepository
    {
        public DiaryEventRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(DiaryEventType))}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[DiaryEventType]", "[DiaryEventType].[Id]", "[DiaryEvent].[EventTypeId]")}";
        }

        protected override async Task<IEnumerable<DiaryEvent>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<DiaryEvent, DiaryEventType, DiaryEvent>(sql, (diaryEvent, type) =>
            {
                diaryEvent.EventType = type;
                return diaryEvent;
            }, param);
        }
    }
}
