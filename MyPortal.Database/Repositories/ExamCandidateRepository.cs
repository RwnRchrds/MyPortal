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
    public class ExamCandidateRepository : BaseReadWriteRepository<ExamCandidate>, IExamCandidateRepository
    {
        public ExamCandidateRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Students", "S", "StudentId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "S");

            return query;
        }

        protected override async Task<IEnumerable<ExamCandidate>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var candidates = await Transaction.Connection.QueryAsync<ExamCandidate, Student, ExamCandidate>(sql.Sql,
                (candidate, student) =>
                {
                    candidate.Student = student;

                    return candidate;
                }, sql.NamedBindings, Transaction);

            return candidates;
        }

        public async Task Update(ExamCandidate entity)
        {
            var candidate = await Context.ExamCandidates.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (candidate == null)
            {
                throw new EntityNotFoundException("Candidate not found.");
            }

            candidate.Uci = entity.Uci;
            candidate.CandidateNumber = entity.CandidateNumber;
            candidate.PreviousCandidateNumber = entity.PreviousCandidateNumber;
            candidate.PreviousCentreNumber = entity.PreviousCentreNumber;
            candidate.SpecialConsideration = entity.SpecialConsideration;
            candidate.Note = entity.Note;
        }
    }
}