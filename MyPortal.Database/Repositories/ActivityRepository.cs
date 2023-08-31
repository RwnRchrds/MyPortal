using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ActivityRepository : BaseStudentGroupRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }
        
        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("StudentGroups as SG", "SG.Id", $"{TblAlias}.StudentGroupId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudentGroup), "SG");

            return query;
        }

        protected override async Task<IEnumerable<Activity>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var activities = await DbUser.Transaction.Connection.QueryAsync<Activity, StudentGroup, Activity>(sql.Sql,
                (activity, group) =>
                {
                    activity.StudentGroup = group;

                    return activity;
                }, sql.NamedBindings, DbUser.Transaction);

            return activities;
        }
    }
}
