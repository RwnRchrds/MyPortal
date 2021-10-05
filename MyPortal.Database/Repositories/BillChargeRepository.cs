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
    public class BillChargeRepository : BaseReadWriteRepository<BillCharge>, IBillChargeRepository
    {
        public BillChargeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Bills", "B", "BillId");
            JoinEntity(query, "Charges", "C", "ChargeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Bill), "B");
            query.SelectAllColumns(typeof(Charge), "C");

            return query;
        }

        protected override async Task<IEnumerable<BillCharge>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var billCharges = await Transaction.Connection.QueryAsync<BillCharge, Bill, Charge, BillCharge>(sql.Sql,
                (bc, bill, charge) =>
                {
                    bc.Bill = bill;
                    bc.Charge = charge;

                    return bc;
                }, sql.NamedBindings, Transaction);

            return billCharges;
        }
    }
}