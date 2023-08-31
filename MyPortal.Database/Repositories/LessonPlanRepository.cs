using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class LessonPlanRepository : BaseReadWriteRepository<LessonPlan>, ILessonPlanRepository
    {
        public LessonPlanRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Directories as D", "D.Id", $"{TblAlias}.DirectoryId");
            query.LeftJoin("Users as U", "U.Id", $"{TblAlias}.CreatedById");
            query.LeftJoin("StudyTopics as ST", "ST.Id", $"{TblAlias}.StudyTopicId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Directory), "D");
            query.SelectAllColumns(typeof(User), "U");
            query.SelectAllColumns(typeof(StudyTopic), "ST");

            return query;
        }

        protected override async Task<IEnumerable<LessonPlan>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var lessonPlans =
                await DbUser.Transaction.Connection.QueryAsync<LessonPlan, Directory, User, StudyTopic, LessonPlan>(
                    sql.Sql,
                    (plan, directory, user, studyTopic) =>
                    {
                        plan.Directory = directory;
                        plan.CreatedBy = user;
                        plan.StudyTopic = studyTopic;

                        return plan;
                    }, sql.NamedBindings, DbUser.Transaction);

            return lessonPlans;
        }

        public async Task Update(LessonPlan entity)
        {
            var lessonPlan = await DbUser.Context.LessonPlans.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (lessonPlan == null)
            {
                throw new EntityNotFoundException("Lesson plan not found.");
            }

            lessonPlan.StudyTopicId = entity.StudyTopicId;
            lessonPlan.Title = entity.Title;
            lessonPlan.PlanContent = entity.PlanContent;
        }
    }
}