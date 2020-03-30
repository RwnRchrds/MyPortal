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
    public class ProfileLogNoteRepository : BaseReadWriteRepository<ProfileLogNote>, IProfileLogNoteRepository
    {
        public ProfileLogNoteRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(ProfileLogNoteType))},
{EntityHelper.GetUserColumns("User")},
{EntityHelper.GetAllColumns(typeof(Person), "AuthorPerson")},
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Person), "StudentPerson")},
{EntityHelper.GetAllColumns(typeof(AcademicYear))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[ProfileLogNoteType]", "[ProfileLogNoteType].[Id]", "[ProfileLogNote].[TypeId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[ProfileLogNote].[AuthorId]", "User")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[AuthorPerson].[UserId]", "[User].[Id]", "AuthorPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[ProfileLogNote].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[StudentPerson].[Id]", "[Student].[PersonId]", "StudentPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AcademicYear]", "[AcademicYear].[Id]", "[ProfileLogNote].[AcademicYearId]")}";
        }

        protected override async Task<IEnumerable<ProfileLogNote>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection
                .QueryAsync<ProfileLogNote, ProfileLogNoteType, ApplicationUser, Person, Student, Person, AcademicYear,
                    ProfileLogNote>(sql,
                    (note, type, user, authorPerson, student, studentPerson, acadYear) =>
                    {
                        note.ProfileLogNoteType = type;
                        note.Author = user;
                        note.Author.Person = authorPerson;
                        note.Student = student;
                        note.Student.Person = studentPerson;
                        note.AcademicYear = acadYear;

                        return note;
                    }, param);
        }

        public Task<IEnumerable<ProfileLogNote>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "[ProfileLogNote].[StudentId] = @StudentId");
            SqlHelper.Where(ref sql, "[ProfileLogNote].[AcademicYearId] = @AcademicYearId");

            return ExecuteQuery(sql, new {StudentId = studentId, AcademicYearId = academicYearId});
        }
    }
}