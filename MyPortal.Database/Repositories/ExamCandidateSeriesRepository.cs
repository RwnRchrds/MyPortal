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
    public class ExamCandidateSeriesRepository : BaseReadWriteRepository<ExamCandidateSeries>, IExamCandidateSeriesRepository
    {
        public ExamCandidateSeriesRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamSeries as ES", "ES.Id", $"{TblAlias}.SeriesId");
            query.LeftJoin("ExamCandidate as EC", "EC.Id", $"{TblAlias}.CandidateId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamSeries), "ES");
            query.SelectAllColumns(typeof(ExamCandidate), "EC");

            return query;
        }

        protected override async Task<IEnumerable<ExamCandidateSeries>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var candidateSeries =
                await Transaction.Connection
                    .QueryAsync<ExamCandidateSeries, ExamCandidate, ExamSeries, ExamCandidateSeries>(sql.Sql,
                        (cs, candidate, series) =>
                        {
                            cs.Candidate = candidate;
                            cs.Series = series;

                            return cs;
                        }, sql.NamedBindings, Transaction);

            return candidateSeries;
        }

        public async Task Update(ExamCandidateSeries entity)
        {
            var candidateSeries = await Context.ExamCandidateSeries.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (candidateSeries == null)
            {
                throw new EntityNotFoundException("Candidate series link not found.");
            }

            candidateSeries.Flag = entity.Flag;
        }
    }
}