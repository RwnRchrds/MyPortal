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
    public class ClassRepository : BaseReadWriteRepository<Class>, IClassRepository
    {
        public ClassRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Courses as CC", "CC.Id", $"{TblAlias}.CourseId");
            query.LeftJoin("CurriculumGroup as CG", "CG.Id", $"{TblAlias}.CurriculumGroupId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Course), "CC");
            query.SelectAllColumns(typeof(CurriculumGroup), "CG");

            return query;
        }

        protected override async Task<IEnumerable<Class>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var classes = await DbUser.Transaction.Connection.QueryAsync<Class, Course, CurriculumGroup, Class>(sql.Sql,
                (currClass, course, group) =>
                {
                    currClass.Course = course;
                    currClass.Group = group;

                    return currClass;
                }, sql.NamedBindings, DbUser.Transaction);

            return classes;
        }

        public async Task Update(Class entity)
        {
            var currClass = await DbUser.Context.Classes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (currClass == null)
            {
                throw new EntityNotFoundException("Class not found.");
            }

            currClass.Code = entity.Code;
            currClass.CourseId = entity.CourseId;
            currClass.CurriculumGroupId = entity.CurriculumGroupId;
        }
    }
}