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
    public class StaffMemberRepository : BaseReadWriteRepository<StaffMember>, IStaffMemberRepository
    {
        public StaffMemberRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Person));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Person", "Person.Id", "StaffMember.PersonId");
        }

        protected override async Task<IEnumerable<StaffMember>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<StaffMember, Person, StaffMember>(sql.Sql, (staff, person) =>
            {
                staff.Person = person;

                return staff;
            }, sql.NamedBindings);
        }
    }
}