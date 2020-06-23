using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class LessonPlanRepository : BaseReadWriteRepository<LessonPlan>, ILessonPlanRepository
    {
        public LessonPlanRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(StudyTopic));
            query.SelectAll(typeof(ApplicationUser));
            query.SelectAll(typeof(Person));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.StudyTopic", "StudyTopic.Id", "LessonPlan.StudyTopicId");
            query.LeftJoin("dbo.AspNetUsers as User", "User.Id", "LessonPlan.AuthorId");
            query.LeftJoin("dbo.Person", "Person.UserId", "User.Id");
        }

        protected override async Task<IEnumerable<LessonPlan>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<LessonPlan, StudyTopic, ApplicationUser, Person, LessonPlan>(sql.Sql,
                (lessonPlan, topic, author, person) =>
                {
                    lessonPlan.StudyTopic = topic;
                    lessonPlan.Author = author;
                    lessonPlan.Author.Person = person;

                    return lessonPlan;
                }, sql.NamedBindings);
        }
    }
}