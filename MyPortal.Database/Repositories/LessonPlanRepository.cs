using System.Collections.Generic;
using System.Data.Common;
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
    public class LessonPlanRepository : BaseReadWriteRepository<LessonPlan>, ILessonPlanRepository
    {
        public LessonPlanRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "LessonPlan")
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
            query.LeftJoin("Users as User", "User.Id", "LessonPlan.AuthorId");
            query.LeftJoin("People as Person", "Person.UserId", "User.Id");
        }

        protected override async Task<IEnumerable<LessonPlan>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<LessonPlan, StudyTopic, User, Person, LessonPlan>(sql.Sql,
                (lessonPlan, topic, author, person) =>
                {
                    lessonPlan.StudyTopic = topic;
                    lessonPlan.Author = author;
                    lessonPlan.Author.Person = person;

                    return lessonPlan;
                }, sql.NamedBindings, Transaction);
        }

        public async Task Update(LessonPlan entity)
        {
            var lessonPlan = await Context.LessonPlans.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (lessonPlan == null)
            {
                throw new EntityNotFoundException("Lesson plan not found.");
            }

            lessonPlan.StudyTopicId = entity.StudyTopicId;
            lessonPlan.Title = entity.Title;
            lessonPlan.LearningObjectives = entity.LearningObjectives;
            lessonPlan.PlanContent = entity.PlanContent;
            lessonPlan.Homework = entity.Homework;
        }
    }
}