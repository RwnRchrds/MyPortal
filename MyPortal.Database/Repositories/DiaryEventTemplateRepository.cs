using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
    public class DiaryEventTemplateRepository : BaseReadWriteRepository<DiaryEventTemplate>, IDiaryEventTemplateRepository
    {
        public DiaryEventTemplateRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "DiaryEventTypes", "T", "EventTypeId");

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
                await Transaction.Connection.QueryAsync<DiaryEventTemplate, DiaryEventType, DiaryEventTemplate>(sql.Sql,
                    (template, type) =>
                    {
                        template.DiaryEventType = type;

                        return template;
                    }, sql.NamedBindings, Transaction);

            return templates;
        }

        public async Task Update(DiaryEventTemplate entity)
        {
            var template = await Context.DiaryEventTemplates.FirstOrDefaultAsync(x => x.Id == entity.Id);

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