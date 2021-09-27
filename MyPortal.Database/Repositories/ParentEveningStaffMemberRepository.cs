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

        public async Task Update(ParentEveningStaffMember entity)
        {
            throw new System.NotImplementedException();
        }
    }
}