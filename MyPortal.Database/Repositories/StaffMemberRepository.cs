using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StaffMemberRepository : BaseReadWriteRepository<StaffMember>, IStaffMemberRepository
    {
        public StaffMemberRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "StaffMember")
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

            return await Transaction.Connection.QueryAsync<StaffMember, Person, StaffMember>(sql.Sql, (staff, person) =>
            {
                staff.Person = person;

                return staff;
            }, sql.NamedBindings, Transaction);
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

        public async Task Update(StaffMember entity)
        {
            var employee = await Context.StaffMembers.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (employee == null)
            {
                throw new EntityNotFoundException("Staff member not found.");
            }

            employee.LineManagerId = entity.LineManagerId;
            employee.Code = entity.Code;
            employee.BankName = entity.BankName;
            employee.BankAccount = entity.BankAccount;
            employee.BankSortCode = entity.BankSortCode;
            employee.NiNumber = entity.NiNumber;
            employee.Qualifications = entity.Qualifications;
            employee.TeachingStaff = entity.TeachingStaff;
        }
    }
}