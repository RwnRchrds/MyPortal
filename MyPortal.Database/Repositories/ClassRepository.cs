using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ClassRepository : BaseReadWriteRepository<Class>, IClassRepository
    {
        public ClassRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Subject));
            query.SelectAll(typeof(StaffMember), "Teacher");
            query.SelectAll(typeof(Person), "TeacherPerson");
            query.SelectAll(typeof(CurriculumGroup));
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Subject", "Subject.Id", "Class.SubjectId");
            query.LeftJoin("dbo.StaffMember as Teacher", "Teacher.Id", "Class.TeacherId");
            query.LeftJoin("dbo.Person as TeacherPerson", "TeacherPerson.Id", "Teacher.PersonId");
            query.LeftJoin("dbo.CurriculumGroup", "CurriculumGroup.Id", "Class.GroupId");
        }

        protected override async Task<IEnumerable<Class>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Class, Subject, StaffMember, Person, CurriculumGroup, Class>(sql.Sql,
                (currClass, subject, teacher, person, group) =>
                {
                    currClass.Subject = subject;
                    currClass.Group = group;

                    return currClass;
                }, sql.NamedBindings);
        }
    }
}