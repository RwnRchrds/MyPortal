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
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DocumentRepository : BaseReadWriteRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Users as U", "U.Id", $"{TblAlias}.CreatedById");
            query.LeftJoin("Directory as P", "P.Id", $"{TblAlias}.DirectoryId");
            query.LeftJoin("DocumentType as DT", "DT.Id", $"{TblAlias}.DocumentTypeId");
            query.LeftJoin("Files as F", "F.Id", $"{TblAlias}.FileId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(User), "U");
            query.SelectAllColumns(typeof(Directory), "P");
            query.SelectAllColumns(typeof(DocumentType), "DT");
            query.SelectAllColumns(typeof(File), "F");

            return query;
        }

        protected override async Task<IEnumerable<Document>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var documents =
                await DbUser.Transaction.Connection.QueryAsync<Document, User, Directory, DocumentType, File, Document>(
                    sql.Sql,
                    (document, createdBy, dir, type, attachment) =>
                    {
                        document.Type = type;
                        document.Attachment = attachment;
                        document.CreatedBy = createdBy;
                        document.Directory = dir;

                        return document;
                    }, sql.NamedBindings, DbUser.Transaction);

            return documents;
        }

        public async Task<IEnumerable<Document>> GetByDirectory(Guid directoryId)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.DirectoryId", directoryId);

            return await ExecuteQuery(query);
        }

        public async Task Update(Document entity)
        {
            var document = await DbUser.Context.Documents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (document == null)
            {
                throw new EntityNotFoundException("Document not found.");
            }

            document.Title = entity.Title;
            document.Description = entity.Description;
            document.TypeId = entity.TypeId;
            document.Private = entity.Private;
        }

        public async Task UpdateWithAttachment(Document entity)
        {
            var document = await DbUser.Context.Documents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (document == null)
            {
                throw new EntityNotFoundException("Document not found.");
            }

            document.Title = entity.Title;
            document.Description = entity.Description;
            document.TypeId = entity.TypeId;
            document.Private = entity.Private;
            document.Attachment = entity.Attachment;
        }
    }
}