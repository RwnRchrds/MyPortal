using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ClassRepository : BaseReadWriteRepository<Class>, IClassRepository
    {
        public ClassRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
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

            var classes = await Transaction.Connection.QueryAsync<Class, Course, CurriculumGroup, Class>(sql.Sql,
                (currClass, course, group) =>
                {
                    currClass.Course = course;
                    currClass.Group = group;

                    return currClass;
                }, sql.NamedBindings, Transaction);

            return classes;
        }

        public async Task Update(Class entity)
        {
            var currClass = await Context.Classes.FirstOrDefaultAsync(x => x.Id == entity.Id);

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