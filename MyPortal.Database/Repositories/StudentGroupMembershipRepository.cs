using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StudentGroupMembershipRepository : BaseReadWriteRepository<StudentGroupMembership>, IStudentGroupMembershipRepository
    {
        public StudentGroupMembershipRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Students", "S", "StudentId");
            JoinEntity(query, "StudentGroups", "SG", "StudentGroupId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(StudentGroup), "SG");

            return query;
        }
        
        protected override async Task<IEnumerable<StudentGroupMembership>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var memberships = await Transaction.Connection
                .QueryAsync<StudentGroupMembership, Student, StudentGroup, StudentGroupMembership>(sql.Sql,
                    (membership, student, group) =>
                    {
                        membership.Student = student;
                        membership.StudentGroup = group;

                        return membership;
                    }, sql.NamedBindings, Transaction);

            return memberships;
        }

        public async Task Update(StudentGroupMembership entity)
        {
            var membership = await Context.StudentGroupMemberships.FirstOrDefaultAsync(x => x.Id == entity.Id);

            membership.StartDate = entity.StartDate;
            membership.EndDate = entity.EndDate;
        }
    }
}