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
    public class ExamResultEmbargoRepository : BaseReadWriteRepository<ExamResultEmbargo>, IExamResultEmbargoRepository
    {
        public ExamResultEmbargoRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ResultSets as RS", "RS.Id", $"{TblAlias}.ResultSetId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ResultSet), "RS");

            return query;
        }

        protected override async Task<IEnumerable<ExamResultEmbargo>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var embargoes = await Transaction.Connection.QueryAsync<ExamResultEmbargo, ResultSet, ExamResultEmbargo>(
                sql.Sql,
                (embargo, resultSet) =>
                {
                    embargo.ResultSet = resultSet;

                    return embargo;
                }, sql.NamedBindings, Transaction);

            return embargoes;
        }

        public async Task Update(ExamResultEmbargo entity)
        {
            var embargo = await Context.ExamResultEmbargoes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (embargo == null)
            {
                throw new EntityNotFoundException("Result embargo not found.");
            }
            
            embargo.EndTime = entity.EndTime;
        }

        public async Task<ExamResultEmbargo> GetByResultSetId(Guid resultSetId)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.ResultSetId", resultSetId);

            return await ExecuteQueryFirstOrDefault(query);
        }
    }
}