using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ClassRepository : BaseReadWriteRepository<Class>, IClassRepository
    {
        public ClassRepository(IDbConnection connection) : base(connection)
        {
        RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(AcademicYear))},
{EntityHelper.GetAllColumns(typeof(Subject))},
{EntityHelper.GetAllColumns(typeof(StaffMember), "Teacher")},
{EntityHelper.GetAllColumns(typeof(Person), "TeacherPerson")},
{EntityHelper.GetAllColumns(typeof(YearGroup))}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AcademicYear]", "[AcademicYear].[Id]", "[Class].[AcademicYearId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Subject]", "[Subject].[Id]", "[Class].[SubjectId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[Teacher].[Id]", "[Class].[TeacherId]", "Teacher")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[TeacherPerson].[Id]", "[Teacher].[PersonId]", "TeacherPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[YearGroup]", "[YearGroup].[Id]", "[Class].[YearGroupId]")}";
        }

        protected override async Task<IEnumerable<Class>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Class, AcademicYear, Subject, StaffMember, Person, YearGroup, Class>(sql,
                (currClass, acadYear, subject, teacher, person, yearGroup) =>
                {
                    currClass.AcademicYear = acadYear;
                    currClass.Subject = subject;
                    currClass.Teacher = teacher;
                    currClass.Teacher.Person = person;
                    currClass.YearGroup = yearGroup;

                    return currClass;
                }, param);
        }

        public async Task Update(Class entity)
        {
            var classInDb = await Context.Classes.FindAsync(entity.Id);

            classInDb.SubjectId = entity.SubjectId;
            classInDb.Name = entity.Name;
            classInDb.TeacherId = entity.TeacherId;
            classInDb.YearGroupId = entity.YearGroupId;
        }
    }
}