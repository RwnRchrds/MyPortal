using System;
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
        public HomeworkSubmissionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "HomeworkItems", "HI", "HomeworkId");
            JoinEntity(query, "Students", "S", "StudentId");
            JoinEntity(query, "Tasks", "T", "TaskId");
            JoinEntity(query, "Documents", "D", "DocumentId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(HomeworkItem), "HI");
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(Task), "T");
            query.SelectAllColumns(typeof(Document), "D");

            return query;
        }

        protected override async Task<IEnumerable<HomeworkSubmission>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var submissions = await Transaction.Connection
                .QueryAsync<HomeworkSubmission, HomeworkItem, Student, Task, Document, HomeworkSubmission>(sql.Sql,
                    (submission, item, student, task, document) =>
                    {
                        submission.HomeworkItem = item;
                        submission.Student = student;
                        submission.Task = task;
                        submission.SubmittedWork = document;

                        return submission;
                    }, sql.NamedBindings, Transaction);

            return submissions;
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