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
        public HomeworkSubmissionRepository(ApplicationDbContext context) : base(context, "HomeworkSubmission")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(HomeworkItem), "HomeworkItem");
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(Person), "StudentPerson");
            query.SelectAllColumns(typeof(Task), "Task");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("HomeworkItems as HomeworkItem", "HomeworkItem.Id", "HomeworkSubmission.HomeworkId");
            query.LeftJoin("Students as Student", "Student.Id", "HomeworkSubmission.StudentId");
            query.LeftJoin("People as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("Tasks as Task", "Task.Id", "HomeworkSubmission.TaskId");
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