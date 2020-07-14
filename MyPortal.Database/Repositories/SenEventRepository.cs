using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class SenEventRepository : BaseReadWriteRepository<SenEvent>, ISenEventRepository
    {
        public SenEventRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(SenEventType));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Student", "Student.Id", "SenEvent.StudentId");
            query.LeftJoin("SenEventType", "SenEventType.Id", "SenEvent.EventTypeId");
        }

        protected override async Task<IEnumerable<SenEvent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<SenEvent, Student, SenEventType, SenEvent>(sql.Sql,
                (senEvent, student, type) =>
                {
                    senEvent.Student = student;
                    senEvent.Type = type;

                    return senEvent;
                }, sql.NamedBindings);
        }
    }
}