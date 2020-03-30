using System.Collections.Generic;
using System.Data;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class TaskRepository : BaseReadWriteRepository<Task>, ITaskRepository
    {
        public TaskRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Person))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[Task].[AssignedToId]")}";
        }

        protected override async System.Threading.Tasks.Task<IEnumerable<Task>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Task, Person, Task>(sql, (task, person) =>
            {
                task.Person = person;

                return task;
            }, param);
        }
    }
}