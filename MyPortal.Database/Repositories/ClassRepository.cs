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
            query.SelectAll(typeof(AcademicYear));
            query.SelectAll(typeof(Subject));
            query.SelectAll(typeof(StaffMember), "Teacher");
            query.SelectAll(typeof(Person), "TeacherPerson");
            query.SelectAll(typeof(YearGroup));
            query.SelectAll(typeof(CurriculumBand));
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.AcademicYear", "AcademicYear.Id", "Class.AcademicYearId");
            query.LeftJoin("dbo.Subject", "Subject.Id", "Class.SubjectId");
            query.LeftJoin("dbo.StaffMember as Teacher", "Teacher.Id", "Class.TeacherId");
            query.LeftJoin("dbo.Person as TeacherPerson", "TeacherPerson.Id", "Teacher.PersonId");
            query.LeftJoin("dbo.YearGroup", "YearGroup.Id", "Class.YearGroupId");
            query.LeftJoin("dbo.CurriculumBand", "CurriculumBand.Id", "Class.BandId");
        }

        protected override async Task<IEnumerable<Class>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Class, AcademicYear, Subject, StaffMember, Person, YearGroup, CurriculumBand, Class>(sql.Sql,
                (currClass, acadYear, subject, teacher, person, yearGroup, band) =>
                {
                    currClass.AcademicYear = acadYear;
                    currClass.Subject = subject;
                    currClass.YearGroup = yearGroup;
                    currClass.Band = band;

                    return currClass;
                }, sql.NamedBindings);
        }
    }
}