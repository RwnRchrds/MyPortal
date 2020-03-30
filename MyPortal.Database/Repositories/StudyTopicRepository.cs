using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class StudyTopicRepository : BaseReadWriteRepository<StudyTopic>, IStudyTopicRepository
    {
        public StudyTopicRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Subject))},
{EntityHelper.GetAllColumns(typeof(YearGroup))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Subject]", "[Subject].[Id]", "[StudyTopic].[SubjectId]")},
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[YearGroup]", "[YearGroup].[Id]", "[StudyTopic].[YearGroupId]")}";
        }

        protected override async Task<IEnumerable<StudyTopic>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<StudyTopic, Subject, YearGroup, StudyTopic>(sql,
                (topic, subject, year) =>
                {
                    topic.Subject = subject;
                    topic.YearGroup = year;

                    return topic;
                }, param);
        }
    }
}