﻿using System.Collections.Generic;
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
    public class BillDiscountRepository : BaseReadWriteRepository<BillDiscount>, IBillDiscountRepository
    {
        public BillDiscountRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Bills", "B", "BillId");
            JoinEntity(query, "Discounts", "D", "DiscountId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Bill), "B");
            query.SelectAllColumns(typeof(ChargeDiscount), "D");

            return query;
        }

        protected override async Task<IEnumerable<BillDiscount>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var discounts =
                await Transaction.Connection.QueryAsync<BillDiscount, Bill, Discount, BillDiscount>(
                    sql.Sql,
                    (bd, bill, discount) =>
                    {
                        bd.Bill = bill;
                        bd.Discount = discount;

                        return bd;
                    }, sql.NamedBindings, Transaction);

            return discounts;
        }

        public async Task Update(BillDiscount entity)
        {
            var bcd = await Context.BillChargeDiscounts.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (bcd == null)
            {
                throw new EntityNotFoundException("Bill charge discount not found.");
            }

            bcd.DiscountId = entity.DiscountId;
            bcd.GrossAmount = entity.GrossAmount;
        }
    }
};