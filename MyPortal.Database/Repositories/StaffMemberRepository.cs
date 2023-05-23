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
        public StaffMemberRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");
            query.LeftJoin("StaffMembers as LM", "LM.Id", $"{TblAlias}.LineManagerId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "P");
            query.SelectAllColumns(typeof(StaffMember), "LM");

            return query;
        }

        protected override async Task<IEnumerable<StaffMember>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var staffMembers = await Transaction.Connection.QueryAsync<StaffMember, Person, StaffMember, StaffMember>(
                sql.Sql,
                (staff, person, lineManager) =>
                {
                    staff.Person = person;
                    staff.LineManager = lineManager;

                    return staff;
                }, sql.NamedBindings, Transaction);

            return staffMembers;
        }

        public async Task<StaffMember> GetByPersonId(Guid personId)
        {
            var query = GetDefaultQuery();

            query.Where("P.Id", personId);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }

        public async Task<StaffMember> GetByUserId(Guid userId)
        {
            var query = GetDefaultQuery();

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