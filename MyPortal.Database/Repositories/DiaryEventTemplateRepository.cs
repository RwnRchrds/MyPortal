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
    public class DiaryEventTemplateRepository : BaseReadWriteRepository<DiaryEventTemplate>,
        IDiaryEventTemplateRepository
    {
        public DiaryEventTemplateRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("DiaryEventTypes as T", "T.Id", $"{TblAlias}.EventTypeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(DiaryEventType), "T");

            return query;
        }

        protected override async Task<IEnumerable<DiaryEventTemplate>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var templates =
                await DbUser.Transaction.Connection.QueryAsync<DiaryEventTemplate, DiaryEventType, DiaryEventTemplate>(
                    sql.Sql,
                    (template, type) =>
                    {
                        template.DiaryEventType = type;

                        return template;
                    }, sql.NamedBindings, DbUser.Transaction);

            return templates;
        }

        public async Task Update(DiaryEventTemplate entity)
        {
            var template = await DbUser.Context.DiaryEventTemplates.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (template == null)
            {
                throw new EntityNotFoundException("Event template not found.");
            }

            template.Description = entity.Description;
            template.Active = entity.Active;
            template.Days = entity.Days;
            template.Hours = entity.Hours;
            template.Minutes = entity.Minutes;
            template.EventTypeId = entity.EventTypeId;
        }
    }
}