using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DiaryEventAttendeeRepository : BaseReadWriteRepository<DiaryEventAttendee>,
        IDiaryEventAttendeeRepository
    {
        public DiaryEventAttendeeRepository(IDbConnection connection, ApplicationDbContext context) : base(connection,
            context, "Attendee")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(DiaryEvent), "Event");
            query.SelectAll(typeof(Person));
            query.SelectAll(typeof(DiaryEventAttendeeResponse), "Response");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("DiaryEvent as Event", "Event.Id", "Attendee.EventId");
            query.LeftJoin("Person", "Person.Id", "Attendee.PersonId");
            query.LeftJoin("DiaryEventAttendeeResponse as Response", "Response.Id", "Attendee.ResponseId");
        }

        protected override async Task<IEnumerable<DiaryEventAttendee>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<DiaryEventAttendee, DiaryEvent, Person, DiaryEventAttendeeResponse, DiaryEventAttendee>(
                    sql.Sql,
                    (attendee, diaryEvent, person, response) =>
                    {
                        attendee.Event = diaryEvent;
                        attendee.Person = person;
                        attendee.Response = response;

                        return attendee;
                    }, sql.NamedBindings);
        }

        public async Task<IEnumerable<DiaryEventAttendee>> GetByEvent(Guid eventId)
        {
            var query = SelectAllColumns();

            query.Where("Event.Id", eventId);

            return await ExecuteQuery(query);
        }
    }
}