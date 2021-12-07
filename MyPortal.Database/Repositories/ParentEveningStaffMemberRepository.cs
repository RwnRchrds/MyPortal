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
    public class ParentEveningStaffMemberRepository : BaseReadWriteRepository<ParentEveningStaffMember>, IParentEveningStaffMemberRepository
    {
        public ParentEveningStaffMemberRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "ParentEvenings", "PE", "ParentEveningId");
            JoinEntity(query, "StaffMembers", "SM", "StaffMemberId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ParentEvening), "PE");
            query.SelectAllColumns(typeof(StaffMember), "SM");

            return query;
        }

        protected override async Task<IEnumerable<ParentEveningStaffMember>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var staffMembers =
                await Transaction.Connection
                    .QueryAsync<ParentEveningStaffMember, ParentEvening, StaffMember, ParentEveningStaffMember>(sql.Sql,
                        (pesm, evening, staffMember) =>
                        {
                            pesm.ParentEvening = evening;
                            pesm.StaffMember = staffMember;

                            return pesm;
                        }, sql.NamedBindings, Transaction);

            return staffMembers;
        }

        public async Task<IEnumerable<ParentEveningStaffMember>> GetLinkedParentEveningsByStaffMember(
            Guid staffMemberId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.StaffMemberId");

            return await ExecuteQuery(query);
        }

        public async Task<ParentEveningStaffMember> GetInstanceByStaffMember(Guid parentEveningId, Guid staffMemberId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.ParentEvningId", parentEveningId);
            query.Where($"{TblAlias}.StaffMemberId", staffMemberId);

            return await ExecuteQueryFirstOrDefault(query);
        }

        public async Task Update(ParentEveningStaffMember entity)
        {
            var pesm = await Context.ParentEveningStaffMembers.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (pesm == null)
            {
                throw new EntityNotFoundException("Parent evening staff member not found.");
            }

            pesm.AvailableFrom = entity.AvailableFrom;
            pesm.AvailableTo = entity.AvailableTo;
            pesm.AppointmentLength = entity.AppointmentLength;
            pesm.BreakLimit = entity.BreakLimit;
        }
    }
}