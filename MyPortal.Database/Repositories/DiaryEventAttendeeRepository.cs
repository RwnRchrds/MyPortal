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

        public async Task<IEnumerable<DiaryEventAttendee>> GetByEvent(Guid eventId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.Id", eventId);

            return await ExecuteQuery(query);
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