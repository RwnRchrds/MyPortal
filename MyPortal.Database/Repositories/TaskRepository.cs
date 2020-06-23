using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
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

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Person as AssignedTo", "AssignedTo.Id", "Task.AssignedToId");
            query.LeftJoin("dbo.AspNetUsers as AssignedBy", "AssignedBy.Id", "Task.AssignedById");
        }

        protected override async System.Threading.Tasks.Task<IEnumerable<Task>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Task, Person, ApplicationUser, Task>(sql.Sql, (task, assignedTo, assignedBy) =>
            {
                task.AssignedTo = assignedTo;
                task.AssignedBy = assignedBy;

                return task;
            }, sql.NamedBindings);
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetByAssignedTo(Guid personId)
        {
            var query = SelectAllColumns();

            query.Where("Task.AssignedToId", personId);

            return await ExecuteQuery(query);
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetActiveByAssignedTo(Guid personId)
        {
            var query = SelectAllColumns();

            query.Where("Task.AssignedToId", personId);

            query.Where("Task.Completed", false).OrWhereDate("Task.DueDate", ">", DateTime.Today);

            return await ExecuteQuery(query);
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetCompletedByAssignedTo(Guid personId)
        {
            var query = SelectAllColumns();

            query.Where("Task.AssignedToId", personId);

            query.Where("Task.Completed", true);

            return await ExecuteQuery(query);
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetOverdueByAssignedTo(Guid personId)
        {
            var query = SelectAllColumns();

            query.Where("Task.AssignedToId", personId);
            query.Where("Task.Completed", false);
            query.Where("Task.DueDate", DateTime.Today);

            return await ExecuteQuery(query);
        }
    }
}