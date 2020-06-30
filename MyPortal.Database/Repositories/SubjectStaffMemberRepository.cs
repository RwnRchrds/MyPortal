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
    public class SubjectStaffMemberRepository : BaseReadWriteRepository<SubjectStaffMember>, ISubjectStaffMemberRepository
    {
        public SubjectStaffMemberRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Subject));
            query.SelectAll(typeof(StaffMember));
            query.SelectAll(typeof(Person));
            query.SelectAll(typeof(SubjectStaffMemberRole), "Role");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Subject", "Subject.Id", "SubjectStaffMember.SubjectId");
            query.LeftJoin("dbo.StaffMember", "StaffMember.Id", "SubjectStaffMember.StaffMemberId");
            query.LeftJoin("dbo.Person", "Person.Id", "StaffMember.PersonId");
            query.LeftJoin("dbo.SubjectStaffMemberRole as Role", "Role.Id", "SubjectStaffMember.RoleId");
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