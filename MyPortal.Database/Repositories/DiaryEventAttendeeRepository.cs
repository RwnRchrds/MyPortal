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
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DiaryEventAttendeeRepository : BaseReadWriteRepository<DiaryEventAttendee>,
        IDiaryEventAttendeeRepository
    {
        public DiaryEventAttendeeRepository(ApplicationDbContext context) : base(context, "Attendee")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(DiaryEvent), "Event");
            query.SelectAllColumns(typeof(Person));
            query.SelectAllColumns(typeof(DiaryEventAttendeeResponse), "Response");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("DiaryEvents as Event", "Event.Id", "Attendee.EventId");
            query.LeftJoin("People as Person", "Person.Id", "Attendee.PersonId");
            query.LeftJoin("DiaryEventAttendeeResponses as Response", "Response.Id", "Attendee.ResponseId");
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
            var query = GenerateQuery();

            query.Where("Event.Id", eventId);

            return await ExecuteQuery(query);
        }
    }
}