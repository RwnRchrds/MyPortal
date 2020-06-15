using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;
using Task = MyPortal.Database.Models.Task;

namespace MyPortal.Database.Repositories
{
    public class HomeworkSubmissionRepository : BaseReadWriteRepository<HomeworkSubmission>, IHomeworkSubmissionRepository
    {
        public HomeworkSubmissionRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Homework));
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person), "StudentPerson");
            query.SelectAll(typeof(Task));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Homework", "Homework.Id", "HomeworkSubmission.HomeworkId");
            query.LeftJoin("dbo.Student", "Student.Id", "HomeworkSubmission.StudentId");
            query.LeftJoin("dbo.Person as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("dbo.Task", "Task.Id", "HomeworkSubmission.TaskId");
        }

        protected override async Task<IEnumerable<HomeworkSubmission>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<HomeworkSubmission, Homework, Student, Person, Models.Task, HomeworkSubmission>(sql.Sql,
                    (submission, homework, student, person, task) =>
                    {
                        submission.Homework = homework;
                        submission.Student = student;
                        submission.Student.Person = person;
                        submission.Task = task;

                        return submission;
                    }, sql.Bindings);
        }
    }
}