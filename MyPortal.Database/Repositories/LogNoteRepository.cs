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

namespace MyPortal.Database.Repositories
{
    public class LogNoteRepository : BaseReadWriteRepository<LogNote>, ILogNoteRepository
    {
        public LogNoteRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(LogNoteType))},
{EntityHelper.GetUserColumns("User")},
{EntityHelper.GetAllColumns(typeof(Person), "AuthorPerson")},
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Person), "StudentPerson")},
{EntityHelper.GetAllColumns(typeof(AcademicYear))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[LogNoteType]", "[LogNoteType].[Id]", "[LogNote].[TypeId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[LogNote].[CreatedById]", "User")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[AuthorPerson].[UserId]", "[User].[Id]", "AuthorPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[LogNote].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[StudentPerson].[Id]", "[Student].[PersonId]", "StudentPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AcademicYear]", "[AcademicYear].[Id]", "[LogNote].[AcademicYearId]")}";
        }

        protected override async Task<IEnumerable<LogNote>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection
                .QueryAsync<LogNote, LogNoteType, ApplicationUser, Person, Student, Person, AcademicYear,
                    LogNote>(sql,
                    (note, type, user, authorPerson, student, studentPerson, acadYear) =>
                    {
                        note.LogNoteType = type;
                        note.CreatedBy = user;
                        note.CreatedBy.Person = authorPerson;
                        note.Student = student;
                        note.Student.Person = studentPerson;
                        note.AcademicYear = acadYear;

                        return note;
                    }, param);
        }

        public Task<IEnumerable<LogNote>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "[LogNote].[StudentId] = @StudentId");
            SqlHelper.Where(ref sql, "[LogNote].[AcademicYearId] = @AcademicYearId");

            return ExecuteQuery(sql, new {StudentId = studentId, AcademicYearId = academicYearId});
        }
    }
}