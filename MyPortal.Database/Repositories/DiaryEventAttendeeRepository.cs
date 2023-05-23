using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity;
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
    public class DiaryEventAttendeeRepository : BaseReadWriteRepository<DiaryEventAttendee>,
        IDiaryEventAttendeeRepository
    {
        public DiaryEventAttendeeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("DiaryEvents as DE", "DE.Id", $"{TblAlias}.EventId");
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");
            query.LeftJoin("DiaryEventAttendeeResponses as DEAR", "DEAR.Id", $"{TblAlias}.ResponseId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(DiaryEvent), "DE");
            query.SelectAllColumns(typeof(Person), "P");
            query.SelectAllColumns(typeof(DiaryEventAttendeeResponse), "DEAR");

            return query;
        }

        protected override async Task<IEnumerable<DiaryEventAttendee>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var attendees = await Transaction.Connection
                .QueryAsync<DiaryEventAttendee, DiaryEvent, Person, DiaryEventAttendeeResponse, DiaryEventAttendee>(
                    sql.Sql,
                    (attendee, diaryEvent, person, response) =>
                    {
                        attendee.Event = diaryEvent;
                        attendee.Person = person;
                        attendee.Response = response;

                        return attendee;
                    }, sql.NamedBindings, Transaction);

            return attendees;
        }

        public async Task<IEnumerable<DiaryEventAttendee>> GetByEvent(Guid eventId)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.Id", eventId);

            return await ExecuteQuery(query);
        }

        public async Task<DiaryEventAttendee> GetAttendee(Guid eventId, Guid personId)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.EventId", eventId);
            query.Where($"{TblAlias}.PersonId");

            return await ExecuteQueryFirstOrDefault(query);
        }

        public async Task Update(DiaryEventAttendee entity)
        {
            var attendee = await Context.DiaryEventAttendees.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (attendee == null)
            {
                throw new EntityNotFoundException("Attendee not found.");
            }

            attendee.Attended = entity.Attended;
            attendee.Required = entity.Required;
            attendee.ResponseId = entity.ResponseId;
        }
    }
}