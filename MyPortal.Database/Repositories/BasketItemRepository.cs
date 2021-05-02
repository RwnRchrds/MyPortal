using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class BasketItemRepository : BaseReadWriteRepository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }
        
        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
            query.LeftJoin("Products as P", "P.Id", $"{TblAlias}.ProductId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(Product), "P");

            return query;
        }

        protected override async Task<IEnumerable<BasketItem>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var basketItems = await Transaction.Connection.QueryAsync<BasketItem, Student, Product, BasketItem>(sql.Sql,
                (item, student, product) =>
                {
                    item.Student = student;
                    item.Product = product;

                    return item;
                }, sql.NamedBindings, Transaction);

            return basketItems;
        }

        public async Task Update(BasketItem entity)
        {
            var basketItem = await Context.BasketItems.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (basketItem == null)
            {
                throw new EntityNotFoundException("Basket item not found.");
            }

            basketItem.Quantity = entity.Quantity;
        }
    }
}