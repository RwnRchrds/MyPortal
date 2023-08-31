using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CourseRepository : BaseReadWriteRepository<Course>, ICourseRepository
    {
        public CourseRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Subjects as S", "S.Id", $"{TblAlias}.SubjectId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Subject), "S");

            return query;
        }

        protected override async Task<IEnumerable<Course>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var courses = await DbUser.Transaction.Connection.QueryAsync<Course, Subject, Course>(sql.Sql,
                (course, subject) =>
                {
                    course.Subject = subject;

                    return course;
                }, sql.NamedBindings, DbUser.Transaction);

            return courses;
        }

        public async Task Update(Course entity)
        {
            var course = await DbUser.Context.Courses.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (course == null)
            {
                throw new EntityNotFoundException("Course not found.");
            }

            course.Description = entity.Description;
            course.Active = entity.Active;
            course.Name = entity.Name;
        }
    }
}