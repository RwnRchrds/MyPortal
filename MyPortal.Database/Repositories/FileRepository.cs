using System;
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
    public class FileRepository : BaseReadWriteRepository<File>, IFileRepository
    {
        public FileRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        public async Task<File> GetByDocumentId(Guid documentId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.DocumentId", documentId);

            return await ExecuteQueryFirstOrDefault(query);
        }

        public async Task Update(File entity)
        {
            var file = await Context.Files.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (file == null)
            {
                throw new EntityNotFoundException("File not found.");
            }

            file.FileName = entity.FileName;
            file.ContentType = entity.ContentType;
            file.FileId = entity.FileId;
        }
    }
}
