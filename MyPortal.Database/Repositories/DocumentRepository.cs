using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DocumentRepository : BaseReadWriteRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(DocumentType))},
{EntityHelper.GetUserColumns("User")}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[DocumentType]", "[DocumentType].[Id]", "[Document].[TypeId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[Document].[UploaderId]", "User")}";
        }

        protected override async Task<IEnumerable<Document>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Document, DocumentType, ApplicationUser, Document>(sql,
                (document, type, uploader) =>
                {
                    document.Type = type;
                    document.Uploader = uploader;

                    return document;
                }, param);
        }

        public async Task Update(Document entity)
        {
            var documentInDb = await Context.Documents.FindAsync(entity.Id);

            documentInDb.Title = entity.Title;
            documentInDb.Description = entity.Description;
            documentInDb.TypeId = entity.TypeId;
            documentInDb.DownloadUrl = entity.DownloadUrl;
        }
    }
}