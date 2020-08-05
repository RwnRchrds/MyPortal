using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
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
            query.SelectAllColumns(typeof(Subject));
            query.SelectAllColumns(typeof(YearGroup));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Subjects as Subject", "Subject.Id", "StudyTopic.SubjectId");
            query.LeftJoin("YearGroups as YearGroup", "YearGroup.Id", "StudyTopic.YearGroupId");
        }

        protected override async Task<IEnumerable<StudyTopic>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<StudyTopic, Subject, YearGroup, StudyTopic>(sql.Sql,
                (topic, subject, year) =>
                {
                    topic.Subject = subject;
                    topic.YearGroup = year;

                    return topic;
                }, sql.NamedBindings);
        }
    }
}