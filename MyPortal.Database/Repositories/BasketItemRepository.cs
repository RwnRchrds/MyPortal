using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class BasketItemRepository : BaseReadWriteRepository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Person), "StudentPerson")},
{EntityHelper.GetAllColumns(typeof(Product))}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[BasketItem].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[Student].[PersonId]", "StudentPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Product]", "[Product].[Id]", "[BasketItem].[ProductId]")}";
        }

        protected override async Task<IEnumerable<BasketItem>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<BasketItem, Student, Person, Product, BasketItem>(sql,
                (item, student, person, product) =>
                {
                    item.Student = student;
                    item.Student.Person = person;
                    item.Product = product;

                    return item;
                }, param);
        }

        public async Task Update(BasketItem entity)
        {
            var itemInDb = await Context.BasketItems.FindAsync(entity.Id);

            itemInDb.ProductId = entity.ProductId;
        }
    }
}