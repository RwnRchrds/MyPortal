using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class TaskRepository : BaseReadWriteRepository<Task>, ITaskRepository
    {
        public TaskRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query GetDefaultQuery(bool includeSoftDeleted = false)
        {
            var query = new Query($"{TblName} as {TblAlias}");

            query.LeftJoin("HomeworkSubmissions as HS", "T.Id", "HS.TaskId");
            query.LeftJoin("HomeworkItems as HI", "HS.HomeworkId", "HI.Id");

            query.Select($"{TblAlias}.Id");
            query.Select($"{TblAlias}.TypeId");
            query.Select($"{TblAlias}.Id");
            query.Select($"{TblAlias}.AssignedToId");
            query.Select($"{TblAlias}.CreatedById");
            query.Select($"{TblAlias}.CreatedDate");
            query.Select($"{TblAlias}.DueDate");
            query.Select($"{TblAlias}.CompletedDate");
            query.SelectRaw($"COALESCE(HI.Title, {TblAlias}.Title) as [Title]");
            query.SelectRaw($"COALESCE(HI.Description, {TblAlias}.Description) as [Description]");
            query.Select($"{TblAlias}.Completed");
            query.Select($"{TblAlias}.AllowEdit");
            query.Select($"{TblAlias}.System");

            JoinRelated(query);
            SelectAllRelated(query);

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("People as AT", "AT.Id", $"{TblAlias}.AssignedToId");
            query.LeftJoin("Users as AB", "AB.Id", $"{TblAlias}.CreatedById");
            query.LeftJoin("TaskTypes as TT", "TT.Id", $"{TblAlias}.TypeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "AT");
            query.SelectAllColumns(typeof(User), "AB");
            query.SelectAllColumns(typeof(TaskType), "TT");

            return query;
        }

        protected override async System.Threading.Tasks.Task<IEnumerable<Task>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var tasks = await DbUser.Transaction.Connection.QueryAsync<Task, Person, User, TaskType, Task>(sql.Sql,
                (task, person, user, type) =>
                {
                    task.AssignedTo = person;
                    task.CreatedBy = user;
                    task.Type = type;

                    return task;
                }, sql.NamedBindings, DbUser.Transaction);

            return tasks;
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetByAssignedTo(Guid personId,
            TaskSearchOptions searchOptions = null)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.AssignedToId", personId);

            if (searchOptions != null)
            {
                ApplySearch(query, searchOptions);
            }

            return await ExecuteQuery(query);
        }

        private void ApplySearch(Query query, TaskSearchOptions searchOptions)
        {
            if (searchOptions.Status == TaskStatus.Overdue)
            {
                query.Where($"{TblAlias}.Completed", false);
                query.Where($"{TblAlias}.DueDate", "<", DateTime.Today);
            }

            else if (searchOptions.Status == TaskStatus.Active)
            {
                query.Where($"{TblAlias}.Completed", false);
            }

            else if (searchOptions.Status == TaskStatus.Completed)
            {
                query.Where($"{TblAlias}.Completed", true);
            }
        }

        public async System.Threading.Tasks.Task Update(Task entity)
        {
            var task = await DbUser.Context.Tasks.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (task == null)
            {
                throw new EntityNotFoundException("Task not found.");
            }

            task.Completed = entity.Completed;
            task.Description = entity.Description;
            task.TypeId = entity.TypeId;
            task.DueDate = entity.DueDate;
            task.CompletedDate = entity.CompletedDate;
        }
    }
}