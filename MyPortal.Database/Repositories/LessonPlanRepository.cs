using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class LessonPlanRepository : BaseReadWriteRepository<LessonPlan>, ILessonPlanRepository
    {
        public LessonPlanRepository(ApplicationDbContext context) : base(context, "LessonPlan")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudyTopic), "StudyTopic");
            query.SelectAllColumns(typeof(User), "User");
            query.SelectAllColumns(typeof(Person), "Person");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("StudyTopics as StudyTopic", "StudyTopic.Id", "LessonPlan.StudyTopicId");
            query.LeftJoin("AspNetUsers as User", "User.Id", "LessonPlan.AuthorId");
            query.LeftJoin("People as Person", "Person.UserId", "User.Id");
        }

        protected override async Task<IEnumerable<LessonPlan>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<LessonPlan, StudyTopic, User, Person, LessonPlan>(sql.Sql,
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