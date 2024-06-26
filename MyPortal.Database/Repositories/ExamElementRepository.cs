﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamElementRepository : BaseReadWriteRepository<ExamElement>, IExamElementRepository
    {
        public ExamElementRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamBaseElements as BE", "BE.Id", $"{TblAlias}.BaseElementId");
            query.LeftJoin("ExamSeries as ES", "ES.Id", $"{TblAlias}.SeriesId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamBaseElement), "BE");
            query.SelectAllColumns(typeof(ExamSeries), "ES");

            return query;
        }

        protected override async Task<IEnumerable<ExamElement>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var elements =
                await DbUser.Transaction.Connection.QueryAsync<ExamElement, ExamBaseElement, ExamSeries, ExamElement>(
                    sql.Sql,
                    (element, baseElement, series) =>
                    {
                        element.BaseElement = baseElement;
                        element.Series = series;

                        return element;
                    }, sql.NamedBindings, DbUser.Transaction);

            return elements;
        }

        public async Task Update(ExamElement entity)
        {
            var element = await DbUser.Context.ExamElements.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (element == null)
            {
                throw new EntityNotFoundException("Element not found.");
            }

            element.Description = entity.Description;
            element.ExamFee = entity.ExamFee;
            element.Submitted = entity.Submitted;
        }
    }
}