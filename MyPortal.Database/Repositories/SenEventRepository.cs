using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class SenEventRepository : BaseReadWriteRepository<SenEvent>, ISenEventRepository
    {
        public SenEventRepository(ApplicationDbContext context) : base(context, "SenEvent")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(SenEventType), "SenEventType");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Students as Student", "Student.Id", "SenEvent.StudentId");
            query.LeftJoin("SenEventTypes as SenEventType", "SenEventType.Id", "SenEvent.EventTypeId");
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