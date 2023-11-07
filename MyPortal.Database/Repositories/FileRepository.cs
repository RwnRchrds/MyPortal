using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class FileRepository : BaseReadWriteRepository<File>, IFileRepository
    {
        public FileRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task<File> GetByDocumentId(Guid documentId)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.DocumentId", documentId);

            return await ExecuteQueryFirstOrDefault(query);
        }

        public async Task Update(File entity)
        {
            var file = await DbUser.Context.Files.FirstOrDefaultAsync(x => x.Id == entity.Id);

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