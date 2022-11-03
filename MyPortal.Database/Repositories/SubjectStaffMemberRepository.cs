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
        public SubjectStaffMemberRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Subjects as SU", "SU.Id", $"{TblAlias}.SubjectId");
            query.LeftJoin("StaffMembers as SM", "SM.Id", $"{TblAlias}.StaffMemberId");
            query.LeftJoin("SubjectStaffMemberRoles as SSMR", "SSMR.Id", $"{TblAlias}.RoleId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Subject), "SU");
            query.SelectAllColumns(typeof(StaffMember), "SM");
            query.SelectAllColumns(typeof(SubjectStaffMemberRole), "SSMR");

            return query;
        }

        protected override async Task<IEnumerable<SubjectStaffMember>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var subjectStaff =
                await Transaction.Connection
                    .QueryAsync<SubjectStaffMember, Subject, StaffMember, SubjectStaffMemberRole, SubjectStaffMember>(
                        sql.Sql,
                        (subjectStaffMember, subject, staff, role) =>
                        {
                            subjectStaffMember.Subject = subject;
                            subjectStaffMember.StaffMember = staff;
                            subjectStaffMember.Role = role;

                            return subjectStaffMember;
                        }, sql.NamedBindings, Transaction);

            return subjectStaff;
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