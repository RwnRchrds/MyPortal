using System;
using System.Collections.Generic;
using System.Data.Common;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class TaskRepository : BaseReadWriteRepository<Task>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "People", "AT", "AssignedToId");
            JoinEntity(query, "Users", "AB", "AssignedById");
            JoinEntity(query, "TaskTypes", "TT", "TypeId");

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

            var tasks = await Transaction.Connection.QueryAsync<Task, Person, User, TaskType, Task>(sql.Sql,
                (task, person, user, type) =>
                {
                    task.AssignedTo = person;
                    task.AssignedBy = user;
                    task.Type = type;

                    return task;
                }, sql.NamedBindings, Transaction);

            return tasks;
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetByAssignedTo(Guid personId, TaskSearchOptions searchOptions = null)
        {
            var query = GenerateQuery();

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
            var task = await Context.Tasks.FirstOrDefaultAsync(x => x.Id == entity.Id);

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