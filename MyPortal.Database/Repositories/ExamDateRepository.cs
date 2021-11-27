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
    public class ExamDateRepository : BaseReadWriteRepository<ExamDate>, IExamDateRepository
    {
        public ExamDateRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "ExamSessions", "ES", "SessionId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamSession), "ES");

            return query;
        }

        protected override async Task<IEnumerable<ExamDate>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var examDates = await Transaction.Connection.QueryAsync<ExamDate, ExamSession, ExamDate>(sql.Sql,
                (date, session) =>
                {
                    date.Session = session;

                    return date;
                }, sql.NamedBindings, Transaction);

            return examDates;
        }

        public async Task Update(ExamDate entity)
        {
            var examDate = await Context.ExamDates.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (examDate == null)
            {
                throw new EntityNotFoundException("Exam schedule not found.");
            }

            examDate.Duration = entity.Duration;
            examDate.SessionId = entity.SessionId;
            examDate.SittingDate = entity.SittingDate;
        }
    }
}