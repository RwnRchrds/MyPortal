using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Database.Search;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class TaskRepository : BaseReadWriteRepository<Task>, ITaskRepository
    {
        public TaskRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Person), "AssignedTo");
            query.SelectAll(typeof(ApplicationUser), "AssignedBy");
            query.SelectAll(typeof(Person), "AssignedByPerson");
            query.SelectAll(typeof(TaskType), "Type");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Person as AssignedTo", "AssignedTo.Id", "Task.AssignedToId");
            query.LeftJoin("AspNetUsers as AssignedBy", "AssignedBy.Id", "Task.AssignedById");
            query.LeftJoin("Person as AssignedByPerson", "AssignedByPerson.UserId", "AssignedBy.Id");
            query.LeftJoin("TaskType as Type", "Type.Id", "Task.TypeId");
        }

        protected override async System.Threading.Tasks.Task<IEnumerable<Task>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Task, Person, ApplicationUser, Person, TaskType, Task>(sql.Sql, (task, assignedTo, assignedBy, abp, type) =>
            {
                task.AssignedTo = assignedTo;
                task.AssignedBy = assignedBy;
                task.AssignedBy.Person = abp;
                task.Type = type;

                return task;
            }, sql.NamedBindings);
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
    }
}