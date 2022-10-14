using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
    public class GradeRepository : BaseReadWriteRepository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "GradeSets", "GS", "GradeSetId");

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

            var grades = await Transaction.Connection.QueryAsync<Grade, GradeSet, Grade>(sql.Sql, (grade, set) =>
            {
                grade.GradeSet = set;

                return grade;
            }, sql.NamedBindings, Transaction);

            return grades;
        }

        public async Task Update(Grade entity)
        {
            var grade = await Context.Grades.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (grade == null)
            {
                throw new EntityNotFoundException("Grade not found.");
            }
            
            var gradeSet = await Context.GradeSets.FirstOrDefaultAsync(x => x.Id == grade.GradeSetId);

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
            var query = GenerateQuery();

            query.Where("GS.Id", gradeSetId);

            return await ExecuteQuery(query);
        }
    }
}