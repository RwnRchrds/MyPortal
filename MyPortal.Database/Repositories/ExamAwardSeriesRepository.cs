using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ExamAwardSeriesRepository : BaseReadWriteRepository<ExamAwardSeries>, IExamAwardSeriesRepository
    {
        public ExamAwardSeriesRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "ExamAwards", "EA", "AwardId");
            JoinEntity(query, "ExamSeries", "ES", "SeriesId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamAward), "EA");
            query.SelectAllColumns(typeof(ExamSeries), "ES");

            return query;
        }

        protected override async Task<IEnumerable<ExamAwardSeries>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var examAwardSeries =
                await Transaction.Connection.QueryAsync<ExamAwardSeries, ExamAward, ExamSeries, ExamAwardSeries>(
                    sql.Sql,
                    (eas, award, series) =>
                    {
                        eas.Award = award;
                        eas.Series = series;

                        return eas;
                    }, sql.NamedBindings, Transaction);

            return examAwardSeries;
        }
    }
}