using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ExamCandidateSpecialArrangementRepository : BaseReadWriteRepository<ExamCandidateSpecialArrangement>,
        IExamCandidateSpecialArrangementRepository
    {
        public ExamCandidateSpecialArrangementRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamCandidates as EC", "EC.Id", $"{TblAlias}.CandidateId");
            query.LeftJoin("ExamSpecialArrangements as ESM", "ESM.Id", $"{TblAlias}.SpecialArrangementId");

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

            var candidateSpecialArrangements = await DbUser.Transaction.Connection
                .QueryAsync<ExamCandidateSpecialArrangement, ExamCandidate, ExamSpecialArrangement,
                    ExamCandidateSpecialArrangement>(sql.Sql,
                    (ecsa, candidate, specialArrangement) =>
                    {
                        ecsa.Candidate = candidate;
                        ecsa.SpecialArrangement = specialArrangement;

                        return ecsa;
                    }, sql.NamedBindings, DbUser.Transaction);

            return candidateSpecialArrangements;
        }
    }
}