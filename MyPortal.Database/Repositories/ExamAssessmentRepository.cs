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
    public class ExamAssessmentRepository : BaseReadWriteRepository<ExamAssessment>, IExamAssessmentRepository
    {
        public ExamAssessmentRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamBoards as EB", "EB.Id", $"{TblAlias}.ExamBoardId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamBoard), "EB");

            return query;
        }

        protected override async Task<IEnumerable<ExamAssessment>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var examAssessments = await Transaction.Connection.QueryAsync<ExamAssessment, ExamBoard, ExamAssessment>(
                sql.Sql,
                (assessment, board) =>
                {
                    assessment.ExamBoard = board;

                    return assessment;
                }, sql.NamedBindings, Transaction);

            return examAssessments;
        }

        public async Task Update(ExamAssessment entity)
        {
            var examAssessment = await Context.ExamAssessments.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (examAssessment == null)
            {
                throw new EntityNotFoundException("Assessment not found.");
            }

            examAssessment.ExamBoardId = entity.ExamBoardId;
            examAssessment.AssessmentType = entity.AssessmentType;
            examAssessment.InternalTitle = entity.InternalTitle;
            examAssessment.ExternalTitle = entity.ExternalTitle;
        }
    }
}