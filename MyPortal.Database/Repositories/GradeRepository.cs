using System;
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
    public class GradeRepository : BaseReadWriteRepository<Grade>, IGradeRepository
    {
        public GradeRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("GradeSets as GS", "GS.Id", $"{TblAlias}.GradeSetId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(GradeSet), "GS");

            return query;
        }

        protected override async Task<IEnumerable<Grade>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var grades = await DbUser.Transaction.Connection.QueryAsync<Grade, GradeSet, Grade>(sql.Sql, (grade, set) =>
            {
                grade.GradeSet = set;

                return grade;
            }, sql.NamedBindings, DbUser.Transaction);

            return grades;
        }

        public async Task Update(Grade entity)
        {
            var grade = await DbUser.Context.Grades.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (grade == null)
            {
                throw new EntityNotFoundException("Grade not found.");
            }

            var gradeSet = await DbUser.Context.GradeSets.FirstOrDefaultAsync(x => x.Id == grade.GradeSetId);

            if (gradeSet == null)
            {
                throw new EntityNotFoundException("Grade set not found.");
            }

            if (gradeSet.System)
            {
                throw ExceptionHelper.UpdateSystemEntityException;
            }

            grade.Code = entity.Code;
            grade.Description = entity.Description;
            grade.Value = entity.Value;
        }

        public async Task<IEnumerable<Grade>> GetByGradeSet(Guid gradeSetId)
        {
            var query = GetDefaultQuery();

            query.Where("GS.Id", gradeSetId);

            return await ExecuteQuery(query);
        }
    }
}