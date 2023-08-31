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
    public class ExamSeatAllocationRepository : BaseReadWriteRepository<ExamSeatAllocation>,
        IExamSeatAllocationRepository
    {
        public ExamSeatAllocationRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamComponentSittings as ECS", "ECS.Id", $"{TblAlias}.SittingId");
            query.LeftJoin("ExamCandidates as EC", "EC.Id", $"{TblAlias}.CandidateId");

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
                await DbUser.Transaction.Connection
                    .QueryAsync<ExamSeatAllocation, ExamComponentSitting, ExamCandidate, ExamSeatAllocation>(sql.Sql,
                        (seatAllocation, sitting, candidate) =>
                        {
                            seatAllocation.Sitting = sitting;
                            seatAllocation.Candidate = candidate;

                            return seatAllocation;
                        }, sql.NamedBindings, DbUser.Transaction);

            return seatAllocations;
        }

        public async Task Update(ExamSeatAllocation entity)
        {
            var seatAllocation = await DbUser.Context.ExamSeatAllocations.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (seatAllocation == null)
            {
                throw new EntityNotFoundException("Seat allocation not found.");
            }

            seatAllocation.SittingId = entity.SittingId;
            seatAllocation.SeatRow = entity.SeatRow;
            seatAllocation.SeatColumn = entity.SeatColumn;
            seatAllocation.CandidateId = entity.CandidateId;
            seatAllocation.Active = entity.Active;
            seatAllocation.Attended = entity.Attended;
        }
    }
}