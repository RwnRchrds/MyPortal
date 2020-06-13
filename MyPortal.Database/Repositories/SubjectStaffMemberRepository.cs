﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SubjectStaffMemberRepository : BaseReadWriteRepository<SubjectStaffMember>, ISubjectStaffMemberRepository
    {
        public SubjectStaffMemberRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(Subject))},
{EntityHelper.GetPropertyNames(typeof(StaffMember))},
{EntityHelper.GetPropertyNames(typeof(Person))},
{EntityHelper.GetPropertyNames(typeof(SubjectStaffMemberRole), "Role")}";

            JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Subject]", "[Subject].[Id]", "[SubjectStaffMember].[SubjectId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[StaffMember].[Id]", "[SubjectStaffMember].[StaffMemberId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[StaffMember].[PersonId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[SubjectStaffMemberRole]", "[Role].[Id]", "[SubjectStaffMember].[RoleId]", "Role")}";
        }

        protected override async Task<IEnumerable<SubjectStaffMember>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection
                .QueryAsync<SubjectStaffMember, Subject, StaffMember, Person, SubjectStaffMemberRole, SubjectStaffMember
                >(sql,
                    (subjectStaff, subject, staff, person, role) =>
                    {
                        subjectStaff.Subject = subject;
                        subjectStaff.StaffMember = staff;
                        subjectStaff.StaffMember.Person = person;
                        subjectStaff.Role = role;
                        
                        return subjectStaff;
                    }, param);
        }
    }
}