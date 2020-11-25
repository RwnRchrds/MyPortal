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
    public class StudyTopicRepository : BaseReadWriteRepository<StudyTopic>, IStudyTopicRepository
    {
        public StudyTopicRepository(ApplicationDbContext context) : base(context, "StudyTopic")
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

            return await Connection.QueryAsync<StudyTopic, Course, StudyTopic>(sql.Sql,
                (topic, course) =>
                {
                    topic.Course = course;

                    return topic;
                }, sql.NamedBindings);
        }
    }
}