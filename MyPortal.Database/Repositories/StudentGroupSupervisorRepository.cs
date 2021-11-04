using System.Collections.Generic;
using System.Data.Common;
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
    public class StudentGroupSupervisorRepository : BaseReadWriteRepository<StudentGroupSupervisor>, IStudentGroupSupervisorRepository
    {
        public StudentGroupSupervisorRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "StudentGroups", "SG", "StudentGroupId");
            JoinEntity(query, "StaffMembers", "SM", "SupervisorId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudentGroup), "SG");
            query.SelectAllColumns(typeof(StaffMember), "SM");

            return query;
        }
        
        protected override async Task<IEnumerable<StudentGroupSupervisor>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var supervisors = await Transaction.Connection
                .QueryAsync<StudentGroupSupervisor, StudentGroup, StaffMember,
                    StudentGroupSupervisor>(sql.Sql,
                    (supervisor, group, staff) =>
                    {
                        supervisor.StudentGroup = group;
                        supervisor.Supervisor = staff;

                        return supervisor;
                    }, sql.NamedBindings, Transaction);

            return supervisors;
        }

        public async Task Update(StudentGroupSupervisor entity)
        {
            var supervisor = await Context.StudentGroupSupervisors.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (supervisor == null)
            {
                throw new EntityNotFoundException("Supervisor not found.");
            }

            supervisor.Title = entity.Title;
        }
    }
}