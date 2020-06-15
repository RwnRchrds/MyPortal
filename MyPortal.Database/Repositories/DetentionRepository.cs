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
    public class DetentionRepository : BaseReadWriteRepository<Detention>, IDetentionRepository
    {
        public DetentionRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(DetentionType));
            query.SelectAll(typeof(DiaryEvent));
            query.SelectAll(typeof(StaffMember));
            query.SelectAll(typeof(Person));
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.DetentionType", "DetentionType.Id", "Detention.DetentionTypeId");
            query.LeftJoin("dbo.DiaryEvent", "DiaryEvent.Id", "Detention.EventId");
            query.LeftJoin("dbo.StaffMember", "StaffMember.Id", "Detention.SupervisorId");
            query.LeftJoin("dbo.Person", "Person.Id", "StaffMember.PersonId");
        }

        protected override async Task<IEnumerable<Detention>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Detention, DetentionType, DiaryEvent, StaffMember, Person, Detention>(sql.Sql,
                (detention, type, diaryEvent, supervisor, person) =>
                {
                    detention.Type = type;
                    detention.Event = diaryEvent;
                    detention.Supervisor = supervisor;

                    detention.Supervisor.Person = person;

                    return detention;
                }, sql.Bindings);
        }
    }
}
