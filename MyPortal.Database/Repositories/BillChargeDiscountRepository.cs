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
    public class BillChargeDiscountRepository : BaseReadWriteRepository<BillChargeDiscount>, IBillChargeDiscountRepository
    {
        public BillChargeDiscountRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Bills", "B", "BillId");
            JoinEntity(query, "ChargeDiscounts", "CD", "ChargeDiscountId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Bill), "B");
            query.SelectAllColumns(typeof(ChargeDiscount), "CD");

            return query;
        }

        protected override async Task<IEnumerable<BillChargeDiscount>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var discounts =
                await Transaction.Connection.QueryAsync<BillChargeDiscount, Bill, ChargeDiscount, BillChargeDiscount>(
                    sql.Sql,
                    (bcd, bill, chargeDiscount) =>
                    {
                        bcd.Bill = bill;
                        bcd.ChargeDiscount = chargeDiscount;

                        return bcd;
                    }, sql.NamedBindings, Transaction);

            return discounts;
        }

        public async Task Update(BillChargeDiscount entity)
        {
            var bcd = await Context.BillChargeDiscounts.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (bcd == null)
            {
                throw new EntityNotFoundException("Bill charge discount not found.");
            }

            bcd.ChargeDiscountId = entity.ChargeDiscountId;
            bcd.GrossAmount = entity.GrossAmount;
        }
    }
};