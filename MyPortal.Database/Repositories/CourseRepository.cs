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
    public class CourseRepository : BaseReadWriteRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Subjects", "S", "SubjectId");

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

            var courses = await Transaction.Connection.QueryAsync<Course, Subject, Course>(sql.Sql, (course, subject) =>
            {
                course.Subject = subject;

                return course;
            }, sql.NamedBindings, Transaction);

            return courses;
        }

        public async Task Update(Course entity)
        {
            var course = await Context.Courses.FirstOrDefaultAsync(x => x.Id == entity.Id);

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