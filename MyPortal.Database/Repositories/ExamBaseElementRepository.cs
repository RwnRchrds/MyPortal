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
    public class ExamBaseElementRepository : BaseReadWriteRepository<ExamBaseElement>, IExamBaseElementRepository
    {
        public ExamBaseElementRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamAssessments as EA", "EA.Id", $"{TblAlias}.AssessmentId");
            query.LeftJoin("SubjectCodes as SC", "SC.Id", $"{TblAlias}.QcaCodeId");
            query.LeftJoin("ExamQualificationLevels as EQL", "EQL.Id", $"{TblAlias}.LevelId");

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
                await DbUser.Transaction.Connection
                    .QueryAsync<ExamBaseElement, ExamAssessment, SubjectCode, ExamQualificationLevel, ExamBaseElement>(
                        sql.Sql,
                        (baseElement, assessment, code, level) =>
                        {
                            baseElement.Assessment = assessment;
                            baseElement.QcaCode = code;
                            baseElement.Level = level;

                            return baseElement;
                        }, sql.NamedBindings, DbUser.Transaction);

            return baseElements;
        }

        public async Task Update(ExamBaseElement entity)
        {
            var baseElement = await DbUser.Context.ExamBaseElements.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (baseElement == null)
            {
                throw new EntityNotFoundException("Base element not found.");
            }

            baseElement.LevelId = entity.LevelId;
            baseElement.QcaCodeId = entity.QcaCodeId;
            baseElement.QualAccrNumber = entity.QualAccrNumber;
            baseElement.ElementCode = entity.ElementCode;
        }
    }
}