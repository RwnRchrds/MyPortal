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
    public class ExamCandidateSpecialArrangementRepository : BaseReadWriteRepository<ExamCandidateSpecialArrangement>, IExamCandidateSpecialArrangementRepository
    {
        public ExamCandidateSpecialArrangementRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "ExamCandidates", "EC", "CandidateId");
            JoinEntity(query, "ExamSpecialArrangements", "ESM", "SpecialArrangementId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamCandidate), "EC");
            query.SelectAllColumns(typeof(ExamSpecialArrangement), "ESM");

            return query;
        }

        protected override async Task<IEnumerable<ExamCandidateSpecialArrangement>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var candidateSpecialArrangements = await Transaction.Connection
                .QueryAsync<ExamCandidateSpecialArrangement, ExamCandidate, ExamSpecialArrangement,
                    ExamCandidateSpecialArrangement>(sql.Sql,
                    (ecsa, candidate, specialArrangement) =>
                    {
                        ecsa.Candidate = candidate;
                        ecsa.SpecialArrangement = specialArrangement;

                        return ecsa;
                    }, sql.NamedBindings, Transaction);

            return candidateSpecialArrangements;
        }
    }
}