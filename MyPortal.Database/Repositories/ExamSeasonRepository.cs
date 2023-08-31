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
    public class ExamSeasonRepository : BaseReadWriteRepository<ExamSeason>, IExamSeasonRepository
    {
        public ExamSeasonRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ResultSet as RS", "RS.Id", $"{TblAlias}.ResultSetId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ResultSet), "RS");

            return query;
        }

        protected override async Task<IEnumerable<ExamSeason>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var examSeasons = await DbUser.Transaction.Connection.QueryAsync<ExamSeason, ResultSet, ExamSeason>(sql.Sql,
                (season, resultSet) =>
                {
                    season.ResultSet = resultSet;

                    return season;
                }, sql.NamedBindings, DbUser.Transaction);

            return examSeasons;
        }

        public async Task Update(ExamSeason entity)
        {
            var season = await DbUser.Context.ExamSeasons.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (season == null)
            {
                throw new EntityNotFoundException("Exam season not found.");
            }

            season.Name = entity.Name;
            season.CalendarYear = entity.CalendarYear;
            season.StartDate = entity.StartDate;
            season.EndDate = entity.EndDate;
            season.Description = entity.Description;
            season.Default = entity.Default;
        }
    }
}