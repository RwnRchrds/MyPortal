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

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetByAssignedTo(Guid personId, TaskSearchOptions searchOptions = null)
        {
            var query = GenerateQuery();

            query.Where("Task.AssignedToId", personId);

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
                query.WhereDate("Task.DueDate", "<", DateTime.Today);
            }

            else if (searchOptions.Status == TaskStatus.Active)
            {
                query.Where(q => q.Where("Task.Completed", false).OrWhereDate("Task.DueDate", ">=", DateTime.Today));
            }

            else if (searchOptions.Status == TaskStatus.Completed)
            {
                query.Where("Task.Completed", true);
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