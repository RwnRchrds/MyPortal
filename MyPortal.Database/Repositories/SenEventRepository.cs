using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SenEventRepository : BaseReadWriteRepository<SenEvent>, ISenEventRepository
    {
        public SenEventRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(Student))},
{EntityHelper.GetPropertyNames(typeof(SenEventType))}";

            (query => JoinRelated(query)) = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[SenEvent].[StudentId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[SenEventType]", "[SenEventType].[Id]", "[SenEvent].[EventTypeId]")}";
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
    }
}