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
    public class SenEventRepository : BaseReadWriteRepository<SenEvent>, ISenEventRepository
    {
        public SenEventRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "SenEvent")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(SenEventType), "SenEventType");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Students as Student", "Student.Id", "SenEvent.StudentId");
            query.LeftJoin("SenEventTypes as SenEventType", "SenEventType.Id", "SenEvent.EventTypeId");
        }

        protected override async Task<IEnumerable<SenEvent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<SenEvent, Student, SenEventType, SenEvent>(sql.Sql,
                (senEvent, student, type) =>
                {
                    senEvent.Student = student;
                    senEvent.Type = type;

                    return senEvent;
                }, sql.NamedBindings, Transaction);
        }

        public async Task Update(SenEvent entity)
        {
            var senEvent = await Context.SenEvents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (senEvent == null)
            {
                throw new EntityNotFoundException("SEN event not found.");
            }

            senEvent.Date = entity.Date;
            senEvent.Note = entity.Note;
            senEvent.EventTypeId = entity.EventTypeId;
        }
    }
}