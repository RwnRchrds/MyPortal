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
    public class ResultRepository : BaseReadWriteRepository<Result>, IResultRepository
    {
        public ResultRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "ResultSets", "RS", "ResultSetId");
            JoinEntity(query, "Aspects", "A", "AspectId");
            JoinEntity(query, "Students", "S", "StudentId");
            JoinEntity(query, "Grades", "G", "GradeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ResultSet), "RS");
            query.SelectAllColumns(typeof(Aspect), "A");
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(Grade), "G");

            return query;
        }

        protected override async Task<IEnumerable<Result>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var results = await Transaction.Connection.QueryAsync<Result, ResultSet, Aspect, Student, Grade, Result>(
                sql.Sql,
                (result, set, aspect, student, grade) =>
                {
                    result.ResultSet = set;
                    result.Aspect = aspect;
                    result.Student = student;
                    result.Grade = grade;

                    return result;
                }, sql.NamedBindings, Transaction);

            return results;
        }

        public async Task Update(Result entity)
        {
            var result = await Context.Results.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (result == null)
            {
                throw new EntityNotFoundException("Result not found.");
            }

            result.GradeId = entity.GradeId;
            result.Mark = entity.Mark;
            result.Comments = entity.Comments;
            result.ColourCode = entity.ColourCode;
        }
    }
}