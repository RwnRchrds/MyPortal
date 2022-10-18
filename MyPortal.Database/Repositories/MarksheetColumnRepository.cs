using System;
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
    public class MarksheetColumnRepository : BaseReadWriteRepository<MarksheetColumn>, IMarksheetColumnRepository
    {
        public MarksheetColumnRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "MarksheetTemplates", "MT", "TemplateId");
            JoinEntity(query, "Aspects", "A", "AspectId");
            JoinEntity(query, "ResultSets", "RS", "ResultSetId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(MarksheetTemplate), "MT");
            query.SelectAllColumns(typeof(Aspect), "A");
            query.SelectAllColumns(typeof(ResultSet), "RS");

            return query;
        }

        protected override async Task<IEnumerable<MarksheetColumn>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var marksheetColumns =
                await Transaction.Connection
                    .QueryAsync<MarksheetColumn, MarksheetTemplate, Aspect, ResultSet, MarksheetColumn>(sql.Sql,
                        (column, template, aspect, resultSet) =>
                        {
                            column.Template = template;
                            column.Aspect = aspect;
                            column.ResultSet = resultSet;

                            return column;
                        }, sql.NamedBindings, Transaction);

            return marksheetColumns;
        }

        public async Task Update(MarksheetColumn entity)
        {
            var column = await Context.MarksheetColumns.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (column == null)
            {
                throw new EntityNotFoundException("Marksheet column not found.");
            }

            column.AspectId = entity.AspectId;
            column.ResultSetId = entity.ResultSetId;
            column.DisplayOrder = entity.DisplayOrder;
            column.ReadOnly = entity.ReadOnly;
        }

        public async Task<IEnumerable<MarksheetColumn>> GetByMarksheet(Guid marksheetId)
        {
            var query = GenerateQuery();

            query.LeftJoin("Marksheets as M", "M.MarksheetTemplateId", "MT.Id");

            query.Where("M.Id", marksheetId);

            return await ExecuteQuery(query);
        }
    }
}