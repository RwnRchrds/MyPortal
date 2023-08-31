using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Assessment;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ResultRepository : BaseReadWriteRepository<Result>, IResultRepository
    {
        public ResultRepository(DbUserWithContext dbUser) : base(dbUser)
        {
            
        }
        
        private Query WithResults(Query query, string alias)
        {
            var cteQuery = new Query("Results as R").Distinct();

            cteQuery.Select("R.Id as ResultId", "R.ResultSetId as ResultSetId", "R.StudentId as StudentId",
                "R.AspectId as AspectId", "R.CreatedById as CreatedById", "R.Date as Date", "R.GradeId as GradeId",
                "R.Mark as Mark", "R.Comment as Comment", "R.ColourCode as ColourCode", "R.Note as Note",
                "SN.Name as StudentName");

            cteQuery.SelectRaw("COALESCE(U.UserName, CBN.Name) as CreatedByName");

            cteQuery.LeftJoin("Users as U", "U.Id", "R.CreatedById");
            cteQuery.LeftJoin("Students as S", "S.Id", "R.StudentId");
            cteQuery.ApplyName("SN", "S.PersonId");
            cteQuery.ApplyName("CBN", "U.PersonId", NameFormat.FullNameAbbreviated);
            
            return query.With(alias, cteQuery);
        }

        private Query GenerateDetailsQuery(string alias = "RM")
        {
            var query = new Query();

            WithResults(query, "ResultsMetadataCte");

            query.Select($"{alias}.ResultId", $"{alias}.ResultSetId", $"{alias}.StudentId",
                $"{alias}.AspectId", $"{alias}.CreatedById", $"{alias}.Date", $"{alias}.GradeId",
                $"{alias}.Mark", $"{alias}.Comment", $"{alias}.ColourCode", $"{alias}.Note",
                $"{alias}.StudentName", $"{alias}.CreatedByName");

            query.From($"ResultsMetadataCte as {alias}");

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ResultSets as RS", "RS.Id", $"{TblAlias}.ResultSetId");
            query.LeftJoin("Aspects as A", "A.Id", $"{TblAlias}.AspectId");
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
            query.LeftJoin("Grades as G", "G.Id", $"{TblAlias}.GradeId");
            query.LeftJoin("Users as U", "U.Id", $"{TblAlias}.CreatedById");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ResultSet), "RS");
            query.SelectAllColumns(typeof(Aspect), "A");
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(Grade), "G");
            query.SelectAllColumns(typeof(User), "U");

            return query;
        }

        protected override async Task<IEnumerable<Result>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var results = await DbUser.Transaction.Connection.QueryAsync<Result, ResultSet, Aspect, Student, Grade, User, Result>(
                sql.Sql,
                (result, set, aspect, student, grade, user) =>
                {
                    result.ResultSet = set;
                    result.Aspect = aspect;
                    result.Student = student;
                    result.Grade = grade;
                    result.CreatedBy = user;

                    return result;
                }, sql.NamedBindings, DbUser.Transaction);

            return results;
        }

        public async Task Update(Result entity)
        {
            var result = await DbUser.Context.Results.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (result == null)
            {
                throw new EntityNotFoundException("Result not found.");
            }

            result.CreatedById = entity.CreatedById;
            result.GradeId = entity.GradeId;
            result.Mark = entity.Mark;
            result.Comment = entity.Comment;
            result.ColourCode = entity.ColourCode;
        }

        public async Task<IEnumerable<ResultDetailModel>> GetResultDetailsByMarksheet(Guid marksheetId)
        {
            var query = GenerateDetailsQuery();

            query.Join("MarksheetColumns as MC", "MC.AspectId", "RM.AspectId");
            query.LeftJoin("MarksheetTemplates as MT", "MT.Id", "MC.TemplateId");
            query.LeftJoin("Marksheets as M", "M.MarksheetTemplateId", "MT.Id");

            query.WhereRaw("RM.ResultSetId = MC.ResultSetId");
            query.Where("M.Id", marksheetId);

            return await ExecuteQuery<ResultDetailModel>(query);
        }

        public async Task<Result> GetResult(Guid studentId, Guid aspectId, Guid resultSetId)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.StudentId", studentId);
            query.Where($"{TblAlias}.AspectId", aspectId);
            query.Where($"{TblAlias}.ResultSetId", resultSetId);

            var result = await ExecuteQueryFirstOrDefault(query);

            return result;
        }

        public async Task<IEnumerable<Result>> GetPreviousResults(Guid studentId, Guid aspectId, DateTime dateTo)
        {
            var query = GetDefaultQuery();
            
            query.Where($"{TblAlias}.StudentId", studentId);
            query.Where($"{TblAlias}.AspectId", aspectId);
            query.Where($"{TblAlias}.Date", "<", dateTo);

            var results = await ExecuteQuery(query);

            return results;
        }
    }
}