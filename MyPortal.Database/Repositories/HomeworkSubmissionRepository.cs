using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = MyPortal.Database.Models.Entity.Task;

namespace MyPortal.Database.Repositories
{
    public class HomeworkSubmissionRepository : BaseReadWriteRepository<HomeworkSubmission>, IHomeworkSubmissionRepository
    {
        public HomeworkSubmissionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "HomeworkSubmission")
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

            return await Transaction.Connection
                .QueryAsync<HomeworkSubmission, HomeworkItem, Student, Person, Task, HomeworkSubmission>(sql.Sql,
                    (submission, homework, student, person, task) =>
                    {
                        submission.HomeworkItem = homework;
                        submission.Student = student;
                        submission.Student.Person = person;
                        submission.Task = task;

                        return submission;
                    }, sql.NamedBindings, Transaction);
        }

        public async System.Threading.Tasks.Task Update(HomeworkSubmission entity)
        {
            var homeworkSubmission = await Context.HomeworkSubmissions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (homeworkSubmission == null)
            {
                throw new EntityNotFoundException("Homework submission not found.");
            }

            homeworkSubmission.DocumentId = entity.DocumentId;
            homeworkSubmission.PointsAchieved = entity.PointsAchieved;
        }
    }
}