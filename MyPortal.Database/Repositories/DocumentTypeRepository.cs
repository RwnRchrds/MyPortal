using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Filters;

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

        public async Task<IEnumerable<DocumentType>> Get(DocumentTypeFilter filter)
        {
            var sql = SelectAllColumns();

            if (filter.Active)
            {
                SqlHelper.Where(ref sql, "[DocumentType].[Active] = 1");
            }

            if (filter.Staff)
            {
                SqlHelper.Where(ref sql, "[DocumentType].[Staff] = 1");
            }

            if (filter.Student)
            {
                SqlHelper.Where(ref sql, "[DocumentType].[Student] = 1");
            }

            if (filter.Contact)
            {
                SqlHelper.Where(ref sql, "[DocumentType].[Contact] = 1");
            }

            if (filter.General)
            {
                SqlHelper.Where(ref sql, "[DocumentType].[General] = 1");
            }

            if (filter.Sen)
            {
                SqlHelper.Where(ref sql, "[DocumentType].[Sen] = 1");
            }

            return await ExecuteQuery(sql);
        }
    }
}