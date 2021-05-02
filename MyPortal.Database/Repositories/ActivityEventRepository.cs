using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
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
    public class ActivityEventRepository : BaseReadWriteRepository<ActivityEvent>, IActivityEventRepository
    {
        public ActivityEventRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Activities as A", "A.Id", $"{TblAlias}.ActivityId");
            query.LeftJoin("DiaryEvents as DE", "DE.Id", $"{TblAlias}.DiaryEventId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Activity), "A");
            query.SelectAllColumns(typeof(DiaryEvent), "DE");

            return query;
        }

        protected override async Task<IEnumerable<ActivityEvent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var activityEvents =
                await Transaction.Connection.QueryAsync<ActivityEvent, Activity, DiaryEvent, ActivityEvent>(sql.Sql,
                    (activityEvent, activity, diaryEvent) =>
                    {
                        activityEvent.Activity = activity;
                        activityEvent.Event = diaryEvent;

                        return activityEvent;
                    }, sql.NamedBindings, Transaction);

            return activityEvents;
        }

        public IEnumerable<ActivityEvent> GetByStudent(Guid studentId, DateTime dateFrom, DateTime dateTo)
        {
            throw new NotImplementedException();
        }
    }
}
