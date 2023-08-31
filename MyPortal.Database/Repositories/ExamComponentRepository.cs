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
    public class ExamComponentRepository : BaseReadWriteRepository<ExamComponent>, IExamComponentRepository
    {
        public ExamComponentRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamBaseComponents as EBC", "EBC.Id", $"{TblAlias}.BaseComponentId");
            query.LeftJoin("ExamSeries as ES", "ES.Id", $"{TblAlias}.ExamSeriesId");
            query.LeftJoin("ExamAssessmentModes as EAM", "EAM.Id", $"{TblAlias}.AssessmentModeId");
            query.LeftJoin("ExamDates as ED", "ED.Id", $"{TblAlias}.ExamDateId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamBaseComponent), "EBC");
            query.SelectAllColumns(typeof(ExamSeries), "ES");
            query.SelectAllColumns(typeof(ExamAssessmentMode), "EAM");
            query.SelectAllColumns(typeof(ExamDate), "ED");

            return query;
        }

        protected override async Task<IEnumerable<ExamComponent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var components = await DbUser.Transaction.Connection
                .QueryAsync<ExamComponent, ExamBaseComponent, ExamSeries, ExamAssessmentMode, ExamDate,
                    ExamComponent>(sql.Sql,
                    (component, baseComponent, series, mode, date) =>
                    {
                        component.BaseComponent = baseComponent;
                        component.Series = series;
                        component.AssessmentMode = mode;
                        component.ExamDate = date;

                        return component;
                    }, sql.NamedBindings, DbUser.Transaction);

            return components;
        }

        public async Task Update(ExamComponent entity)
        {
            var component = await DbUser.Context.ExamComponents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (component == null)
            {
                throw new EntityNotFoundException("Component not found.");
            }

            component.AssessmentModeId = entity.AssessmentModeId;
            component.DateDue = entity.DateDue;
            component.DateSubmitted = entity.DateSubmitted;
            component.MaximumMark = entity.MaximumMark;
        }
    }
}