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
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DocumentRepository : BaseReadWriteRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        public async Task<IEnumerable<Document>> GetByDirectory(Guid directoryId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.DirectoryId", directoryId);

            return await ExecuteQuery(query);
        }

        public async Task Update(Document entity)
        {
            var document = await Context.Documents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (document == null)
            {
                throw new EntityNotFoundException("Document not found.");
            }

            document.Title = entity.Title;
            document.Description = entity.Description;
            document.TypeId = entity.TypeId;
            document.Restricted = entity.Restricted;
        }
    }
}