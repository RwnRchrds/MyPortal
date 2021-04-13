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
    public class StudyTopicRepository : BaseReadWriteRepository<StudyTopic>, IStudyTopicRepository
    {
        public StudyTopicRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "StudyTopic")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Course));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Courses as Course", "Course.Id", "StudyTopic.CourseId");
        }

        protected override async Task<IEnumerable<StudyTopic>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<StudyTopic, Course, StudyTopic>(sql.Sql,
                (topic, course) =>
                {
                    topic.Course = course;

                    return topic;
                }, sql.NamedBindings, Transaction);
        }

        public async Task Update(StudyTopic entity)
        {
            var studyTopic = await Context.StudyTopics.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (studyTopic == null)
            {
                throw new EntityNotFoundException("Study topic not found.");
            }

            studyTopic.Name = entity.Name;
            studyTopic.Description = entity.Description;
            studyTopic.Active = entity.Active;
        }
    }
}