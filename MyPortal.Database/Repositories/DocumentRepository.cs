﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DocumentRepository : BaseReadWriteRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "Document")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(DocumentType), "DocumentType");
            query.SelectAllColumns(typeof(User), "User"); 
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("DocumentTypes as DocumentType", "DocumentType.Id", "Document.TypeId");
            query.LeftJoin("Users as User", "User.Id", "Document.CreatedById");
        }

        protected override async Task<IEnumerable<Document>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<Document, DocumentType, User, Document>(sql.Sql,
                (document, type, uploader) =>
                {
                    document.Type = type;
                    document.CreatedBy = uploader;

                    return document;
                }, sql.NamedBindings, Transaction);
        }

        public async Task<IEnumerable<Document>> GetByDirectory(Guid directoryId)
        {
            var query = GenerateQuery();

            query.Where("Document.DirectoryId", directoryId);

            return await ExecuteQuery(query);
        }
    }
}