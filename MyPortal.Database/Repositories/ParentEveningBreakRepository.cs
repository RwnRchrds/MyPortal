using System;
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
    public class ParentEveningBreakRepository : BaseReadWriteRepository<ParentEveningBreak>, IParentEveningBreakRepository
    {
        public ParentEveningBreakRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "ParentEveningStaffMembers", "PESM", "ParentEveningStaffMemberId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ParentEveningStaffMember), "PESM");

            return query;
        }

        protected override async Task<IEnumerable<ParentEveningBreak>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var eveBreaks = await Transaction.Connection
                .QueryAsync<ParentEveningBreak, ParentEveningStaffMember, ParentEveningBreak>(sql.Sql,
                    (eveBreak, staff) =>
                    {
                        eveBreak.ParentEveningStaffMember = staff;

                        return eveBreak;
                    }, sql.NamedBindings, Transaction);

            return eveBreaks;
        }

        public async Task<IEnumerable<ParentEveningBreak>> GetBreaksByStaffMember(Guid parentEveningId,
            Guid staffMemberId)
        {
            var query = GenerateQuery();

            query.Where("PESM.ParentEveningId", parentEveningId);
            query.Where("PESM.StaffMemberId", staffMemberId);

            return await ExecuteQuery(query);
        }

        public async Task Update(ParentEveningBreak entity)
        {
            var eveBreak = await Context.ParentEveningBreaks.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (eveBreak == null)
            {
                throw new EntityNotFoundException("Break not found.");
            }

            eveBreak.Start = entity.Start;
            eveBreak.End = entity.End;
        }
    }
}