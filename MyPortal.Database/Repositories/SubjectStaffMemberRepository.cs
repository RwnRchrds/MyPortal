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
    public class SubjectStaffMemberRepository : BaseReadWriteRepository<SubjectStaffMember>, ISubjectStaffMemberRepository
    {
        public SubjectStaffMemberRepository(ApplicationDbContext context) : base(context, "SubjectStaffMember")
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

            return await Connection
                .QueryAsync<SubjectStaffMember, Subject, StaffMember, Person, SubjectStaffMemberRole, SubjectStaffMember
                >(sql.Sql,
                    (subjectStaff, subject, staff, person, role) =>
                    {
                        subjectStaff.Subject = subject;
                        subjectStaff.StaffMember = staff;
                        subjectStaff.StaffMember.Person = person;
                        subjectStaff.Role = role;
                        
                        return subjectStaff;
                    }, sql.NamedBindings);
        }
    }
}