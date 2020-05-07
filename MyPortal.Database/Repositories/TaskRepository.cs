using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Repositories
{
    public class TaskRepository : BaseReadWriteRepository<Task>, ITaskRepository
    {
        public TaskRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Person), "AssignedTo")},
{EntityHelper.GetUserColumns("AssignedBy")}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[AssignedTo].[Id]", "[Task].[AssignedToId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[AssignedBy].[Id]", "[Task].[AssignedById]")}";
        }

        protected override async System.Threading.Tasks.Task<IEnumerable<Task>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Task, Person, ApplicationUser, Task>(sql, (task, assignedTo, assignedBy) =>
            {
                task.AssignedTo = assignedTo;
                task.AssignedBy = assignedBy;

                return task;
            }, param);
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetByAssignedTo(Guid personId)
        {
            var sql = SelectAllColumns();
                
            SqlHelper.Where(ref sql, "[Task].[AssignedToId] = @PersonId");

            return await ExecuteQuery(sql, new {PersonId = personId});
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetActiveByAssignedTo(Guid personId)
        {
            var sql = SelectAllColumns();
                
            SqlHelper.Where(ref sql, "[Task].[AssignedToId] = @PersonId");
            
            SqlHelper.Where(ref sql, "[Task].[Completed] = 0 OR [Task].[DueDate] > @DateToday");

            return await ExecuteQuery(sql, new {PersonId = personId, DateToday = DateTime.Today});
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetCompletedByAssignedTo(Guid personId)
        {
            var sql = SelectAllColumns();
                
            SqlHelper.Where(ref sql, "[Task].[AssignedToId] = @PersonId");
            
            SqlHelper.Where(ref sql, "[Task].[Completed] = 1");

            return await ExecuteQuery(sql, new {PersonId = personId});
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetOverdueByAssignedTo(Guid personId)
        {
            var sql = SelectAllColumns();
                
            SqlHelper.Where(ref sql, "[Task].[AssignedToId] = @PersonId");
            
            SqlHelper.Where(ref sql, "[Task].[Completed] = 0");
            
            SqlHelper.Where(ref sql, "[Task].[DueDate] <= @DateToday");

            return await ExecuteQuery(sql, new {PersonId = personId, DateToday = DateTime.Today});
        }
    }
}