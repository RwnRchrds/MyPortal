using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Filters;

namespace MyPortal.Database.Repositories
{
    public class DocumentTypeRepository : BaseReadRepository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(IDbConnection connection) : base(connection, "DocumentType")
        {
        }

        public async Task<IEnumerable<DocumentType>> Get(DocumentTypeFilter filter)
        {
            var query = GenerateQuery();

            if (filter.Active)
            {
                query.Where("DocumentType.Active", true);
            }

            if (filter.Staff)
            {
                query.Where("DocumentType.Staff", true);
            }

            if (filter.Student)
            {
                query.Where("DocumentType.Student", true);
            }

            if (filter.Contact)
            {
                query.Where("DocumentType.Contact", true);
            }

            if (filter.General)
            {
                query.Where("DocumentType.General", true);
            }

            if (filter.Sen)
            {
                query.Where("DocumentType.Sen", true);
            }

            return await ExecuteQuery(query);
        }
    }
}