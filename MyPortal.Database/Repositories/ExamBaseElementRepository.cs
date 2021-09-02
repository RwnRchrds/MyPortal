using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamBaseElementRepository : BaseReadWriteRepository<ExamBaseElement>, IExamBaseElementRepository
    {
        public ExamBaseElementRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "ExamAssessments", "EA", "AssessmentId");
            JoinEntity(query, "SubjectCodes", "SC", "QcaCodeId");
            JoinEntity(query, "ExamQualificationLevels", "EQL", "LevelId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamAssessment), "EA");
            query.SelectAllColumns(typeof(SubjectCode), "SC");
            query.SelectAllColumns(typeof(ExamQualificationLevel), "EQL");

            return query;
        }

        protected override async Task<IEnumerable<ExamBaseElement>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var baseElements =
                await Transaction.Connection
                    .QueryAsync<ExamBaseElement, ExamAssessment, SubjectCode, ExamQualificationLevel, ExamBaseElement>(
                        sql.Sql,
                        (baseElement, assessment, code, level) =>
                        {
                            baseElement.Assessment = assessment;
                            baseElement.QcaCode = code;
                            baseElement.Level = level;

                            return baseElement;
                        }, sql.NamedBindings, Transaction);

            return baseElements;
        }

        public async Task Update(ExamBaseElement entity)
        {
            var baseElement = await Context.ExamBaseElements.FirstOrDefaultAsync(x => x.Id == entity.Id);

            baseElement.LevelId = entity.LevelId;
            baseElement.QcaCodeId = entity.QcaCodeId;
            baseElement.QualAccrNumber = entity.QualAccrNumber;
            baseElement.ElementCode = entity.ElementCode;
        }
    }
}