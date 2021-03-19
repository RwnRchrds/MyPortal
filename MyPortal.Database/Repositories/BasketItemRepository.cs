﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class BasketItemRepository : BaseReadWriteRepository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "BasketItem")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(Person), "StudentPerson");
            query.SelectAllColumns(typeof(Product), "Product");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Students as Student", "Student.Id", "BasketItem.StudentId");
            query.LeftJoin("People as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("Products as Product", "Product.Id", "BasketItem.ProductId");
        }

        protected override async Task<IEnumerable<BasketItem>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<BasketItem, Student, Person, Product, BasketItem>(sql.Sql,
                (item, student, person, product) =>
                {
                    item.Student = student;
                    item.Student.Person = person;
                    item.Product = product;

                    return item;
                }, sql.NamedBindings, Transaction);
        }
    }
}