using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class HomeworkAttachmentRepository : BaseReadWriteRepository<HomeworkAttachment>, IHomeworkAttachmentRepository
    {
        public HomeworkAttachmentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Homework))},
{EntityHelper.GetAllColumns(typeof(Document))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Homework]", "[Homework].[Id]", "[HomeworkAttachment].[HomeworkId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Document]", "[Document].[Id]", "[HomeworkAttachment].[DocumentId]")}";
        }

        protected override async Task<IEnumerable<HomeworkAttachment>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<HomeworkAttachment, Homework, Document, HomeworkAttachment>(sql,
                (attachment, homework, document) =>
                {
                    attachment.Homework = homework;
                    attachment.Document = document;

                    return attachment;
                });
        }
    }
}