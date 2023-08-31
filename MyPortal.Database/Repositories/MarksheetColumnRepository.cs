using System;
using System.Collections.Generic;
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
    public class MarksheetColumnRepository : BaseReadWriteRepository<MarksheetColumn>, IMarksheetColumnRepository
    {
        public MarksheetColumnRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("MarksheetTemplates as MT", "MT.Id", $"{TblAlias}.TemplateId");
            query.LeftJoin("Aspects as A", "A.Id", $"{TblAlias}.AspectId");
            query.LeftJoin("ResultSets as RS", "RS.Id", $"{TblAlias}.ResultSetId");

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
                await DbUser.Transaction.Connection
                    .QueryAsync<MarksheetColumn, MarksheetTemplate, Aspect, ResultSet, MarksheetColumn>(sql.Sql,
                        (column, template, aspect, resultSet) =>
                        {
                            column.Template = template;
                            column.Aspect = aspect;
                            column.ResultSet = resultSet;

                            return column;
                        }, sql.NamedBindings, DbUser.Transaction);

            return marksheetColumns;
        }

        public async Task Update(MarksheetColumn entity)
        {
            var column = await DbUser.Context.MarksheetColumns.FirstOrDefaultAsync(x => x.Id == entity.Id);

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
            var query = GetDefaultQuery();

            query.LeftJoin("Marksheets as M", "M.MarksheetTemplateId", "MT.Id");

            query.Where("M.Id", marksheetId);

            return await ExecuteQuery(query);
        }
    }
}