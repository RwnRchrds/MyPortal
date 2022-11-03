using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class LessonPlanHomeworkItemRepository : BaseReadWriteRepository<LessonPlanHomeworkItem>, ILessonPlanHomeworkItemRepository
    {
        public LessonPlanHomeworkItemRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("LessonPlans as LP", "LP.Id", $"{TblAlias}.LessonPlanId");
            query.LeftJoin("HomeworkItems as HI", "HI.Id", $"{TblAlias}.HomeworkItemId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(LessonPlan), "LP");
            query.SelectAllColumns(typeof(HomeworkItem), "HI");

            return query;
        }
        
        protected override async Task<IEnumerable<LessonPlanHomeworkItem>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var homeworkItems =
                await Transaction.Connection
                    .QueryAsync<LessonPlanHomeworkItem, LessonPlan, HomeworkItem, LessonPlanHomeworkItem>(sql.Sql,
                        (lphi, lessonPlan, homeworkItem) =>
                        {
                            lphi.LessonPlan = lessonPlan;
                            lphi.HomeworkItem = homeworkItem;

                            return lphi;
                        }, sql.NamedBindings, Transaction);

            return homeworkItems;
        }
    }
}