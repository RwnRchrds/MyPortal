using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StudentGroupMembershipRepository : BaseReadWriteRepository<StudentGroupMembership>, IStudentGroupMembershipRepository
    {
        public StudentGroupMembershipRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
            query.LeftJoin("StudentGroups as SG", "SG.Id", $"{TblAlias}.StudentGroupId");

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

            var memberships = await DbUser.Transaction.Connection
                .QueryAsync<StudentGroupMembership, Student, StudentGroup, StudentGroupMembership>(sql.Sql,
                    (membership, student, group) =>
                    {
                        membership.Student = student;
                        membership.StudentGroup = group;

                        return membership;
                    }, sql.NamedBindings, DbUser.Transaction);

            return memberships;
        }

        public async Task Update(StudentGroupMembership entity)
        {
            var membership = await DbUser.Context.StudentGroupMemberships.FirstOrDefaultAsync(x => x.Id == entity.Id);

            membership.StartDate = entity.StartDate;
            membership.EndDate = entity.EndDate;
        }

        public async Task<IEnumerable<StudentGroupMembership>> GetMembershipsByGroup(Guid studentGroupId, DateTime dateFrom, DateTime dateTo)
        {
            var query = GetDefaultQuery();

            query.Where("SGM.StudentGroupId", studentGroupId);
            query.Where("SGM.StartDate", "<=", dateFrom);
            query.Where("SGM.EndDate", ">", dateTo);

            return await ExecuteQuery(query);
        }
    }
}