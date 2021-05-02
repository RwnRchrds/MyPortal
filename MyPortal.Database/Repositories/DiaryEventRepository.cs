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

        public async Task<IEnumerable<DiaryEvent>> GetByDateRange(DateTime firstDate, DateTime lastDate, bool includePrivateEvents = false)
        {
            var query = GenerateQuery();

            query.WhereDate("DiaryEvent.StartTime", ">=", firstDate.Date);
            query.WhereDate("DiaryEvent.EndTime", "<", lastDate.Date.AddDays(1));

            if (!includePrivateEvents)
            {
                query.WhereTrue("DiaryEvent.IsPublic");
            }

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<DiaryEvent>> GetByPerson(DateTime firstDate, DateTime lastDate, Guid personId, bool includeDeclined = false)
        {
            var query = GenerateQuery();

            query.LeftJoin("DiaryEventAttendees as A", "A.EventId", "DiaryEvent.Id");
            
            query.WhereDate("DiaryEvent.StartTime", ">=", firstDate.Date);
            query.WhereDate("DiaryEvent.EndTime", "<", lastDate.Date.AddDays(1));

            query.Where("A.PersonId", personId);

            if (!includeDeclined)
            {
                query.Where(q => q.Where("A.ResponseId", "<>", AttendeeResponses.Declined).WhereFalse("A.Required"));
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
            diaryEvent.IsBlock = entity.IsBlock;
            diaryEvent.IsPublic = entity.IsPublic;
        }
    }
}
