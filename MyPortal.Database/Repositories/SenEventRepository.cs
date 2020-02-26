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
    public class SenEventRepository : BaseReadWriteRepository<SenEvent>, ISenEventRepository
    {
        public SenEventRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(SenEventType))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[SenEvent].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[SenEventType]", "[SenEventType].[Id]", "[SenEvent].[EventTypeId]")}";
        }

        protected override async Task<IEnumerable<SenEvent>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<SenEvent, Student, SenEventType, SenEvent>(sql,
                (senEvent, student, type) =>
                {
                    senEvent.Student = student;
                    senEvent.Type = type;

                    return senEvent;
                }, param);
        }

        public async Task Update(SenEvent entity)
        {
            var senEvent = await Context.SenEvents.FindAsync(entity.Id);

            senEvent.Date = entity.Date;
            senEvent.EventTypeId = entity.EventTypeId;
            senEvent.Note = entity.Note;
        }
    }
}