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
    public class ExamAssessmentAspectRepository : BaseReadWriteRepository<ExamAssessmentAspect>,
        IExamAssessmentAspectRepository
    {
        public ExamAssessmentAspectRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Aspects as A", "A.Id", $"{TblAlias}.AspectId");
            query.LeftJoin("ExamAssessments as EA", "EA.Id", $"{TblAlias}.AssessmentId");
            query.LeftJoin("ExamSeries as ES", "ES.Id", $"{TblAlias}.SeriesId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Aspect), "A");
            query.SelectAllColumns(typeof(ExamAssessment), "EA");
            query.SelectAllColumns(typeof(ExamSeries), "ES");

            return query;
        }

        protected override async Task<IEnumerable<ExamAssessmentAspect>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var examAssessmentAspects =
                await DbUser.Transaction.Connection
                    .QueryAsync<ExamAssessmentAspect, ExamAssessment, Aspect, ExamSeries, ExamAssessmentAspect>(sql.Sql,
                        (eaa, assessment, aspect, series) =>
                        {
                            eaa.Assessment = assessment;
                            eaa.Aspect = aspect;
                            eaa.Series = series;

                            return eaa;
                        }, sql.NamedBindings, DbUser.Transaction);

            return examAssessmentAspects;
        }

        public async Task Update(ExamAssessmentAspect entity)
        {
            var assessmentAspect =
                await DbUser.Context.ExamAssessmentAspects.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (assessmentAspect == null)
            {
                throw new EntityNotFoundException("Assessment aspect not found.");
            }

            assessmentAspect.AspectOrder = entity.AspectOrder;
        }
    }
}