using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
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
            query.SelectAll(typeof(HomeworkItem));
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person), "StudentPerson");
            query.SelectAll(typeof(Task));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("HomeworkItem", "HomeworkItem.Id", "HomeworkSubmission.HomeworkId");
            query.LeftJoin("Student", "Student.Id", "HomeworkSubmission.StudentId");
            query.LeftJoin("Person as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("Task", "Task.Id", "HomeworkSubmission.TaskId");
        }

        protected override async Task<IEnumerable<HomeworkSubmission>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<HomeworkSubmission, HomeworkItem, Student, Person, Models.Task, HomeworkSubmission>(sql.Sql,
                    (submission, homework, student, person, task) =>
                    {
                        submission.HomeworkItem = homework;
                        submission.Student = student;
                        submission.Student.Person = person;
                        submission.Task = task;

                        return submission;
                    }, sql.NamedBindings);
        }
    }
}