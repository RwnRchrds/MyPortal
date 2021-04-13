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
    public class SubjectStaffMemberRepository : BaseReadWriteRepository<SubjectStaffMember>, ISubjectStaffMemberRepository
    {
        public SubjectStaffMemberRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "SubjectStaffMember")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Subject), "Subject");
            query.SelectAllColumns(typeof(StaffMember), "StaffMember");
            query.SelectAllColumns(typeof(Person), "Person");
            query.SelectAllColumns(typeof(SubjectStaffMemberRole), "Role");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Subjects as Subject", "Subject.Id", "SubjectStaffMember.SubjectId");
            query.LeftJoin("StaffMembers as StaffMember", "StaffMember.Id", "SubjectStaffMember.StaffMemberId");
            query.LeftJoin("People as Person", "Person.Id", "StaffMember.PersonId");
            query.LeftJoin("SubjectStaffMemberRole as Role", "Role.Id", "SubjectStaffMember.RoleId");
        }

        protected override async Task<IEnumerable<SubjectStaffMember>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection
                .QueryAsync<SubjectStaffMember, Subject, StaffMember, Person, SubjectStaffMemberRole, SubjectStaffMember
                >(sql.Sql,
                    (subjectStaff, subject, staff, person, role) =>
                    {
                        subjectStaff.Subject = subject;
                        subjectStaff.StaffMember = staff;
                        subjectStaff.StaffMember.Person = person;
                        subjectStaff.Role = role;
                        
                        return subjectStaff;
                    }, sql.NamedBindings, Transaction);
        }

        public async Task Update(SubjectStaffMember entity)
        {
            var subjectStaffMember = await Context.SubjectStaffMembers.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (subjectStaffMember == null)
            {
                throw new EntityNotFoundException("Subject staff member not found.");
            }

            subjectStaffMember.RoleId = entity.RoleId;
        }
    }
}