using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = MyPortal.Database.Models.Entity.Task;

namespace MyPortal.Database.Repositories
{
    public class HomeworkSubmissionRepository : BaseReadWriteRepository<HomeworkSubmission>,
        IHomeworkSubmissionRepository
    {
        public HomeworkSubmissionRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("HomeworkItems", "HI.Id", $"{TblAlias}.HomeworkId");
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
            query.LeftJoin("Tasks as T", "T.Id", $"{TblAlias}.TaskId");
            query.LeftJoin("Documents as D", "D.Id", $"{TblAlias}.DocumentId");

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

            var submissions = await DbUser.Transaction.Connection
                .QueryAsync<HomeworkSubmission, HomeworkItem, Student, Task, Document, HomeworkSubmission>(sql.Sql,
                    (submission, item, student, task, document) =>
                    {
                        submission.HomeworkItem = item;
                        submission.Student = student;
                        submission.Task = task;
                        submission.SubmittedWork = document;

                        return submission;
                    }, sql.NamedBindings, DbUser.Transaction);

            return submissions;
        }

        public async System.Threading.Tasks.Task Update(HomeworkSubmission entity)
        {
            var homeworkSubmission =
                await DbUser.Context.HomeworkSubmissions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (homeworkSubmission == null)
            {
                throw new EntityNotFoundException("Homework submission not found.");
            }

            homeworkSubmission.DocumentId = entity.DocumentId;
            homeworkSubmission.PointsAchieved = entity.PointsAchieved;
        }

        public async Task<IEnumerable<HomeworkSubmission>> GetHomeworkSubmissionsByStudent(Guid studentId)
        {
            var query = GetDefaultQuery();

            query.Where("S.Id", studentId);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<HomeworkSubmission>> GetHomeworkSubmissionsByStudentGroup(Guid studentGroupId)
        {
            var now = DateTime.Now;

            var query = GetDefaultQuery();

            query.JoinStudentGroupsByStudent("S", "SGM");

            query.WhereStudentGroup("SGM", studentGroupId, now);

            return await ExecuteQuery(query);
        }
    }
}