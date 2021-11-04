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
    public class ActivityRepository : BaseStudentGroupRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
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

            var activities = await Transaction.Connection.QueryAsync<Activity, StudentGroup, Activity>(sql.Sql,
                (activity, group) =>
                {
                    activity.StudentGroup = group;

                    return activity;
                }, sql.NamedBindings, Transaction);

            return activities;
        }
    }
}
