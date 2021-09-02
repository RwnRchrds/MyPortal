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
    public class ExamComponentRepository : BaseReadWriteRepository<ExamComponent>, IExamComponentRepository
    {
        public ExamComponentRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "ExamBaseComponents", "EBC", "BaseComponentId");
            JoinEntity(query, "ExamSeries", "ES", "ExamSeriesId");
            JoinEntity(query, "ExamAssessmentModes", "EAM", "AssessmentModeId");
            JoinEntity(query, "ExamSessions", "S", "SessionId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamBaseComponent), "EBC");
            query.SelectAllColumns(typeof(ExamSeries), "ES");
            query.SelectAllColumns(typeof(ExamAssessmentMode), "EAM");
            query.SelectAllColumns(typeof(ExamSession), "S");

            return query;
        }

        protected override async Task<IEnumerable<ExamComponent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var components = await Transaction.Connection
                .QueryAsync<ExamComponent, ExamBaseComponent, ExamSeries, ExamAssessmentMode, ExamSession,
                    ExamComponent>(sql.Sql,
                    (component, baseComponent, series, mode, session) =>
                    {
                        component.BaseComponent = baseComponent;
                        component.Series = series;
                        component.AssessmentMode = mode;
                        component.Session = session;

                        return component;
                    }, sql.NamedBindings, Transaction);

            return components;
        }

        public async Task Update(ExamComponent entity)
        {
            var component = await Context.ExamComponents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (component == null)
            {
                throw new EntityNotFoundException("Component not found.");
            }

            component.AssessmentModeId = entity.AssessmentModeId;
            component.DateDue = entity.DateDue;
            component.DateSubmitted = entity.DateSubmitted;
            component.IsTimetabled = entity.IsTimetabled;
            component.MaximumMark = entity.MaximumMark;
            component.SessionId = entity.SessionId;
            component.Duration = entity.Duration;
            component.SittingDate = entity.SittingDate;
        }
    }
}