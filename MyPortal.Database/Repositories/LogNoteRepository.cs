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
    public class LogNoteRepository : BaseReadWriteRepository<LogNote>, ILogNoteRepository
    {
        public LogNoteRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(LogNoteType));
            query.SelectAll(typeof(ApplicationUser));
            query.SelectAll(typeof(Person), "AuthorPerson");
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person), "StudentPerson");
            query.SelectAll(typeof(AcademicYear));
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.LogNoteType", "LogNoteType.Id", "LogNote.TypeId");
            query.LeftJoin("dbo.AspNetUsers as User", "User.Id", "LogNote.CreatedById");
            query.LeftJoin("dbo.Person as AuthorPerson", "AuthorPerson.UserId", "User.Id");
            query.LeftJoin("dbo.Student", "Student.Id", "LogNote.StudentId");
            query.LeftJoin("dbo.Person as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("dbo.AcademicYear", "AcademicYear.Id", "LogNote.AcademicYearId");
        }

        protected override async Task<IEnumerable<LogNote>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<LogNote, LogNoteType, ApplicationUser, Person, Student, Person, AcademicYear,
                    LogNote>(sql.Sql,
                    (note, type, user, authorPerson, student, studentPerson, acadYear) =>
                    {
                        note.LogNoteType = type;
                        note.CreatedBy = user;
                        note.CreatedBy.Person = authorPerson;
                        note.Student = student;
                        note.Student.Person = studentPerson;
                        note.AcademicYear = acadYear;

                        return note;
                    }, sql.Bindings);
        }

        public Task<IEnumerable<LogNote>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var query = SelectAllColumns();

            query.Where("LogNote.StudentId", studentId);
            query.Where("LogNote.AcademicYearId", academicYearId);

            return ExecuteQuery(query);
        }
    }
}