using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class BasketItemRepository : BaseReadWriteRepository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person), "StudentPerson");
            query.SelectAll(typeof(Product));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Student", "Student.Id", "BasketItem.StudentId");
            query.LeftJoin("dbo.Person as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("dbo.Product", "Product.Id", "BasketItem.ProductId");
        }

        protected override async Task<IEnumerable<BasketItem>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<BasketItem, Student, Person, Product, BasketItem>(sql.Sql,
                (item, student, person, product) =>
                {
                    item.Student = student;
                    item.Student.Person = person;
                    item.Product = product;

                    return item;
                }, sql.Bindings);
        }
    }
}