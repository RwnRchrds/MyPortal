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
    public class StoreDiscountRepository : BaseReadWriteRepository<StoreDiscount>, IStoreDiscountRepository
    {
        public StoreDiscountRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Discounts", "D", "DiscountId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Discount), "D");

            return query;
        }
        
        protected override async Task<IEnumerable<StoreDiscount>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var storeDiscounts = await Transaction.Connection.QueryAsync<StoreDiscount, Discount, StoreDiscount>(
                sql.Sql,
                (sd, discount) =>
                {
                    sd.Discount = discount;

                    return sd;
                }, sql.NamedBindings, Transaction);

            return storeDiscounts;
        }

        public async Task Update(StoreDiscount entity)
        {
            var storeDiscount = await Context.StoreDiscounts.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (storeDiscount == null)
            {
                throw new EntityNotFoundException("Store discount not found.");
            }

            storeDiscount.DiscountId = entity.DiscountId;
            storeDiscount.MinQuantity = entity.MinQuantity;
            storeDiscount.MaxQuantity = entity.MaxQuantity;
            storeDiscount.Auto = entity.Auto;
        }
    }
}