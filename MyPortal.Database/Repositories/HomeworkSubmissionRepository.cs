using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class HomeworkSubmissionRepository : BaseReadWriteRepository<HomeworkSubmission>, IHomeworkSubmissionRepository
    {
        public HomeworkSubmissionRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(Homework))},
{EntityHelper.GetPropertyNames(typeof(Student))},
{EntityHelper.GetPropertyNames(typeof(Person), "StudentPerson")}
{EntityHelper.GetPropertyNames(typeof(Models.Task))}";

            JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Homework]", "[Homework].[Id]", "[HomeworkSubmission].[HomeworkId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[HomeworkSubmission].[StudentId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[StudentPerson].[Id]", "[Student].[PersonId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Task]", "[Task].[Id]", "[HomeworkSubmission].[TaskId]")}";
        }

        protected override async Task<IEnumerable<HomeworkSubmission>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection
                .QueryAsync<HomeworkSubmission, Homework, Student, Person, Models.Task, HomeworkSubmission>(sql,
                    (submission, homework, student, person, task) =>
                    {
                        submission.Homework = homework;
                        submission.Student = student;
                        submission.Student.Person = person;
                        submission.Task = task;

                        return submission;
                    });
        }
    }
}