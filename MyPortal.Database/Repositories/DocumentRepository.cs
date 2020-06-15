using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DocumentRepository : BaseReadWriteRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(DocumentType));
            query.SelectAll(typeof(ApplicationUser), "User"); 
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.DocumentType", "DocumentType.Id", "Document.TypeId");
            query.LeftJoin("dbo.AspNetUsers as User", "User.Id", "Document.UploaderId");
        }

        protected override async Task<IEnumerable<Document>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Document, DocumentType, ApplicationUser, Document>(sql.Sql,
                (document, type, uploader) =>
                {
                    document.Type = type;
                    document.Uploader = uploader;

                    return document;
                }, sql.Bindings);
        }

        public async Task<IEnumerable<Document>> GetByDirectory(Guid directoryId)
        {
            var query = SelectAllColumns();

            query.Where("Document.DirectoryId", directoryId);

            return await ExecuteQuery(query);
        }
    }
}