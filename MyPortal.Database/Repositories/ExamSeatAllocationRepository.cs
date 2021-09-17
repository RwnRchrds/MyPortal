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
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamSeatAllocationRepository : BaseReadWriteRepository<ExamSeatAllocation>, IExamSeatAllocationRepository
    {
        public ExamSeatAllocationRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "ExamComponentSittings", "ECS", "SittingId");
            JoinEntity(query, "ExamCandidates", "EC", "CandidateId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamComponentSitting), "ECS");
            query.SelectAllColumns(typeof(ExamCandidate), "EC");

            return query;
        }

        protected override async Task<IEnumerable<ExamSeatAllocation>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var seatAllocations =
                await Transaction.Connection
                    .QueryAsync<ExamSeatAllocation, ExamComponentSitting, ExamCandidate, ExamSeatAllocation>(sql.Sql,
                        (seatAllocation, sitting, candidate) =>
                        {
                            seatAllocation.Sitting = sitting;
                            seatAllocation.Candidate = candidate;

                            return seatAllocation;
                        }, sql.NamedBindings, Transaction);

            return seatAllocations;
        }

        public async Task Update(ExamSeatAllocation entity)
        {
        }
    }
}