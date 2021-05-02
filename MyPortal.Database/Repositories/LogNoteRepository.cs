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
    public class LogNoteRepository : BaseReadWriteRepository<LogNote>, ILogNoteRepository
    {
        public LogNoteRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        public Task<IEnumerable<LogNote>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.StudentId", studentId);
            query.Where($"{TblAlias}.AcademicYearId", academicYearId);

            return ExecuteQuery(query);
        }

        public async Task Update(LogNote entity)
        {
            var logNote = await Context.ProfileLogNotes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (logNote == null)
            {
                throw new EntityNotFoundException("Log note not found.");
            }

            logNote.UpdatedById = entity.UpdatedById;
            logNote.Message = entity.Message;
            logNote.UpdatedDate = entity.UpdatedDate;
            logNote.TypeId = entity.TypeId;
        }
    }
}