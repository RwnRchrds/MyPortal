using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
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
    public class ChargeDiscountRepository : BaseReadWriteRepository<ChargeDiscount>, IChargeDiscountRepository
    {
        public ChargeDiscountRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Charges as C", "C.Id", $"{TblAlias}.ChargeId");
            query.LeftJoin("Discounts as D", "D.Id", $"{TblAlias}.DiscountId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Charge), "C");
            query.SelectAllColumns(typeof(Discount), "D");

            return query;
        }

        protected override async Task<IEnumerable<ChargeDiscount>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var chargeDiscounts =
                await Transaction.Connection.QueryAsync<ChargeDiscount, Charge, Discount, ChargeDiscount>(sql.Sql,
                    (chargeDiscount, charge, discount) =>
                    {
                        chargeDiscount.Charge = charge;
                        chargeDiscount.Discount = discount;

                        return chargeDiscount;
                    }, sql.NamedBindings, Transaction);

            return chargeDiscounts;
        }

        public async Task<IEnumerable<ChargeDiscount>> GetByDiscount(Guid discountId)
        {
            throw new NotImplementedException();
        }
    }
}
