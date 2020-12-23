using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class FileRepository : BaseReadWriteRepository<File>, IFileRepository
    {
        public FileRepository(ApplicationDbContext context) : base(context, "File")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Document), "Document");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Documents as Document", "Document.Id", "File.DocumentId");
        }

        protected override async Task<IEnumerable<File>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<File, Document, File>(sql.Sql, (file, document) =>
            {
                file.Document = document;

                return file;
            }, sql.NamedBindings);
        }


        public async Task<File> GetByDocumentId(Guid documentId)
        {
            var query = GenerateQuery();

            query.Where("DocumentId", documentId);

            return await ExecuteQueryFirstOrDefault(query);
        }
    }
}
