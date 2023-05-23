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

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Users as U", "U.Id", $"{TblAlias}.CreatedById");
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
            query.LeftJoin("AcademicYears as AY", "AY.Id", $"{TblAlias}.AcademicYearId");
            query.LeftJoin("LogNoteTypes as LNT", "LNT.Id", $"{TblAlias}.TypeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(User), "U");
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(AcademicYear), "AY");
            query.SelectAllColumns(typeof(LogNoteType), "LNT");

            return query;
        }

        protected override async Task<IEnumerable<LogNote>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var logNotes =
                await Transaction.Connection.QueryAsync<LogNote, User, Student, AcademicYear, LogNoteType, LogNote>(
                    sql.Sql,
                    (note, user, student, year, type) =>
                    {
                        note.CreatedBy = user;
                        note.Student = student;
                        note.AcademicYear = year;
                        note.LogNoteType = type;

                        return note;
                    }, sql.NamedBindings, Transaction);

            return logNotes;
        }

        public Task<IEnumerable<LogNote>> GetByStudent(Guid studentId, Guid academicYearId, bool includePrivate)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.StudentId", studentId);
            query.Where($"{TblAlias}.AcademicYearId", academicYearId);

            if (!includePrivate)
            {
                query.Where($"{TblAlias}.Private", false);
            }

            return ExecuteQuery(query);
        }

        public async Task Update(LogNote entity)
        {
            var logNote = await Context.LogNotes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (logNote == null)
            {
                throw new EntityNotFoundException("Log note not found.");
            }
            
            logNote.Message = entity.Message;
            logNote.TypeId = entity.TypeId;
            logNote.Private = entity.Private;
        }
    }
}