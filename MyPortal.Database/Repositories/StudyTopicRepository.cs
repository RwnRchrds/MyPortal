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
        public StudyTopicRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Subject));
            query.SelectAll(typeof(YearGroup));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Subject", "Subject.Id", "StudyTopic.SubjectId");
            query.LeftJoin("YearGroup", "YearGroup.Id", "StudyTopic.YearGroupId");
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