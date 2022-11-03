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
    public class ExamSeriesRepository : BaseReadWriteRepository<ExamSeries>, IExamSeriesRepository
    {
        public ExamSeriesRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamSeasons as S", "S.Id", $"{TblAlias}.ExamSeasonId");
            query.LeftJoin("ExamBoards as EB", "EB.Id", $"{TblAlias}.ExamBoardId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamSeason), "S");
            query.SelectAllColumns(typeof(ExamBoard), "EB");

            return query;
        }

        protected override async Task<IEnumerable<ExamSeries>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var series = await Transaction.Connection.QueryAsync<ExamSeries, ExamSeason, ExamBoard, ExamSeries>(sql.Sql,
                (examSeries, season, board) =>
                {
                    examSeries.Season = season;
                    examSeries.ExamBoard = board;

                    return examSeries;
                }, sql.NamedBindings, Transaction);

            return series;
        }

        public async Task Update(ExamSeries entity)
        {
            var series = await Context.ExamSeries.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (series == null)
            {
                throw new EntityNotFoundException("Exam series not found.");
            }

            series.SeriesCode = entity.SeriesCode;
            series.Code = entity.Code;
            series.Title = entity.Title;
        }
    }
}