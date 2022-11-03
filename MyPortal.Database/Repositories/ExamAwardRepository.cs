using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamAwardRepository : BaseReadWriteRepository<ExamAward>, IExamAwardRepository
    {
        public ExamAwardRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamAssessments as EAS", "EAS.Id", $"{TblAlias}.AssessmentId");
            query.LeftJoin("ExamQualifications as EQ", "EQ.Id", $"{TblAlias}.QualificationId");
            query.LeftJoin("Courses as C", "C.Id", $"{TblAlias}.CourseId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamAssessment), "EAS");
            query.SelectAllColumns(typeof(ExamQualification), "EQ");
            query.SelectAllColumns(typeof(Course), "C");

            return query;
        }

        protected override async Task<IEnumerable<ExamAward>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var examAwards = await Transaction.Connection
                .QueryAsync<ExamAward, ExamAssessment, ExamQualification, Course, ExamAward>(sql.Sql,
                    (award, assessment, qualification, course) =>
                    {
                        award.Assessment = assessment;
                        award.Qualification = qualification;
                        award.Course = course;

                        return award;
                    }, sql.NamedBindings, Transaction);

            return examAwards;
        }

        public async Task Update(ExamAward entity)
        {
            var examAward = await Context.ExamAwards.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (examAward == null)
            {
                throw new EntityNotFoundException("Award not found.");
            }

            examAward.QualificationId = entity.QualificationId;
            examAward.CourseId = entity.CourseId;
            examAward.Description = entity.Description;
            examAward.AwardCode = entity.AwardCode;
            examAward.ExpiryDate = entity.ExpiryDate;
        }
    }
}