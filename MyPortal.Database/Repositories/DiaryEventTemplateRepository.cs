using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DiaryEventTemplateRepository : BaseReadWriteRepository<DiaryEventTemplate>, IDiaryEventTemplateRepository
    {
        public DiaryEventTemplateRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
            RelatedColumns = $@"{EntityHelper.GetAllColumns(typeof(DiaryEventType), "EventType")}";
            JoinRelated =
                $@"{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[DiaryEventType]", "[EventType].[Id]", "[DiaryEventTemplate].[EventTypeId]", "EventType")}";
        }

        protected override async Task<IEnumerable<DiaryEventTemplate>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<DiaryEventTemplate, DiaryEventType, DiaryEventTemplate>(sql,
                (template, eventType) =>
                {
                    template.DiaryEventType = eventType;

                    return template;
                }, param);
        }

        public async Task Update(DiaryEventTemplate entity)
        {
            var templateInDb = await Context.DiaryEventTemplates.FindAsync(entity.Id);

            templateInDb.Description = entity.Description;
            templateInDb.Minutes = entity.Minutes;
            templateInDb.Hours = entity.Hours;
            templateInDb.Days = entity.Days;
        }
    }
}