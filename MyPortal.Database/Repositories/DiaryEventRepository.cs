using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Constants;
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
    public class DiaryEventRepository : BaseReadWriteRepository<DiaryEvent>, IDiaryEventRepository
    {
        public DiaryEventRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "DiaryEventTypes", "DET", "EventTypeId");
            JoinEntity(query, "Rooms", "R", "RoomId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(DiaryEventType), "DET");
            query.SelectAllColumns(typeof(Room), "R");

            return query;
        }

        protected override async Task<IEnumerable<DiaryEvent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var events = await Transaction.Connection.QueryAsync<DiaryEvent, DiaryEventType, Room, DiaryEvent>(sql.Sql,
                (diaryEvent, type, room) =>
                {
                    diaryEvent.EventType = type;
                    diaryEvent.Room = room;

                    return diaryEvent;
                }, sql.NamedBindings, Transaction);

            return events;
        }

        public async Task<IEnumerable<DiaryEvent>> GetByDateRange(DateTime firstDate, DateTime lastDate, bool includePrivateEvents = false)
        {
            var query = GenerateQuery();

            query.WhereDate($"{TblAlias}.StartTime", ">=", firstDate.Date);
            query.WhereDate($"{TblAlias}.EndTime", "<", lastDate.Date.AddDays(1));

            if (!includePrivateEvents)
            {
                query.WhereTrue($"{TblAlias}.IsPublic");
            }

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<DiaryEvent>> GetByPerson(DateTime firstDate, DateTime lastDate, Guid personId, bool includeDeclined = false, bool includePublic = false)
        {
            var query = GenerateQuery();

            query.LeftJoin("DiaryEventAttendees as A", "A.EventId", $"{TblAlias}.Id");
            
            query.WhereDate($"{TblAlias}.StartTime", ">=", firstDate.Date);
            query.WhereDate($"{TblAlias}.EndTime", "<", lastDate.Date.AddDays(1));

            if (includePublic)
            {
                query.Where(q => q.Where("A.PersonId", personId).OrWhereTrue($"{TblAlias}.Public"));
            }
            else
            {
                query.Where("A.PersonId", personId);
            }

            if (!includeDeclined)
            {
                query.Where(q => q.Where("A.ResponseId", "<>", AttendeeResponses.Declined).OrWhereTrue("A.Required"));
            }

            return await ExecuteQuery(query);
        }

        public async Task Update(DiaryEvent entity)
        {
            var diaryEvent = await Context.DiaryEvents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (diaryEvent == null)
            {
                throw new EntityNotFoundException("Calendar event not found.");
            }

            diaryEvent.EventTypeId = entity.EventTypeId;
            diaryEvent.RoomId = entity.RoomId;
            diaryEvent.Subject = entity.Subject;
            diaryEvent.Description = entity.Description;
            diaryEvent.Location = entity.Location;
            diaryEvent.StartTime = entity.StartTime;
            diaryEvent.EndTime = entity.EndTime;
            diaryEvent.IsAllDay = entity.IsAllDay;
            diaryEvent.IsPublic = entity.IsPublic;
        }
    }
}
