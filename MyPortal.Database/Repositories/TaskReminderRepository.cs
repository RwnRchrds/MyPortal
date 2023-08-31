using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;
using MyPortalTask = MyPortal.Database.Models.Entity.Task;

namespace MyPortal.Database.Repositories;

public class TaskReminderRepository : BaseReadWriteRepository<TaskReminder>, ITaskReminderRepository
{
    public TaskReminderRepository(DbUserWithContext dbUser) : base(dbUser)
    {
    }

    protected override Query JoinRelated(Query query)
    {
        query.LeftJoin("Tasks as T", "TR.TaskId", "T.Id");

        return query;
    }

    protected override Query SelectAllRelated(Query query)
    {
        query.SelectAllColumns(typeof(MyPortalTask), "T");

        return query;
    }

    protected override async Task<IEnumerable<TaskReminder>> ExecuteQuery(Query query)
    {
        var sql = Compiler.Compile(query);

        var taskReminders = await DbUser.Transaction.Connection.QueryAsync<TaskReminder, MyPortalTask, TaskReminder>(sql.Sql,
            (reminder, task) =>
            {
                reminder.Task = task;

                return reminder;
            }, sql.NamedBindings, DbUser.Transaction);

        return taskReminders;
    }

    public async Task Update(TaskReminder entity)
    {
        var taskReminder = await DbUser.Context.TaskReminders.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (taskReminder == null)
        {
            throw new EntityNotFoundException("Task reminder not found.");
        }

        taskReminder.TaskId = entity.TaskId;
        taskReminder.UserId = entity.UserId;
        taskReminder.RemindTime = entity.RemindTime;
    }

    public async Task<IEnumerable<TaskReminder>> GetRemindersByTask(Guid taskId)
    {
        var query = GetDefaultQuery();

        query.Where("TR.TaskId", taskId);
        query.Where("T.Completed", false);

        return await ExecuteQuery(query);
    }

    public async Task<IEnumerable<TaskReminder>> GetRemindersByUser(Guid userId)
    {
        var query = GetDefaultQuery();

        query.Where("TR.UserId", userId);
        query.Where("T.Completed", false);

        return await ExecuteQuery(query);
    }
}