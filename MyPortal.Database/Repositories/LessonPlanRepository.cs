using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class LessonPlanRepository : BaseReadWriteRepository<LessonPlan>, ILessonPlanRepository
    {
        public LessonPlanRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(StudyTopic))},
{EntityHelper.GetUserColumns("User")},
{EntityHelper.GetAllColumns(typeof(Person))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StudyTopic]", "[StudyTopic].[Id]", "[LessonPlan].[StudyTopicId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[LessonPlan].[AuthorId]", "User")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[UserId]", "[User].[Id]")}";
        }

        protected override async Task<IEnumerable<LessonPlan>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<LessonPlan, StudyTopic, ApplicationUser, Person, LessonPlan>(sql,
                (lp, topic, author, person) =>
                {
                    lp.StudyTopic = topic;
                    lp.Author = author;
                    lp.Author.Person = person;

                    return lp;
                }, param);
        }

        public async Task Update(LessonPlan entity)
        {
            var lp = await Context.LessonPlans.FindAsync(entity.Id);

            lp.Title = entity.Title;
            lp.LearningObjectives = entity.LearningObjectives;
            lp.PlanContent = entity.PlanContent;
            lp.StudyTopicId = entity.StudyTopicId;
            lp.Homework = entity.Homework;
        }
    }
}