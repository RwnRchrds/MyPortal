using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Repositories
{
    public class LessonPlanRepository : BaseReadWriteRepository<LessonPlan>, ILessonPlanRepository
    {
        public LessonPlanRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(StudyTopic))},
{EntityHelper.GetUserProperties("User")},
{EntityHelper.GetPropertyNames(typeof(Person))}";

            (query => JoinRelated(query)) = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[StudyTopic]", "[StudyTopic].[Id]", "[LessonPlan].[StudyTopicId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[LessonPlan].[AuthorId]", "User")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[UserId]", "[User].[Id]")}";
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
    }
}