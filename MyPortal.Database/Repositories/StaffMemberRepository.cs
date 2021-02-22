using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class StaffMemberRepository : BaseReadWriteRepository<StaffMember>, IStaffMemberRepository
    {
        public StaffMemberRepository(ApplicationDbContext context) : base(context, "StaffMember")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "Person");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("People as Person", "Person.Id", "StaffMember.PersonId");
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

        public async Task<StaffMember> GetByPersonId(Guid personId)
        {
            var query = GenerateQuery();

            query.Where("Person.Id", personId);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }

        public async Task<StaffMember> GetByUserId(Guid userId)
        {
            var query = GenerateQuery();

            query.Where("Person.UserId", userId);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }
    }
}