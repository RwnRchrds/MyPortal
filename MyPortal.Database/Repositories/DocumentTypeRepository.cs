using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class DocumentTypeRepository : BaseReadRepository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }

        protected override async Task<IEnumerable<DocumentType>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<DocumentType>(sql, param);
        }
    }
}