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
using MyPortal.Database.Models.Filters;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DocumentTypeRepository : BaseReadWriteRepository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context,
            transaction, "DocumentType")
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

        public async Task Update(DocumentType entity)
        {
            var documentType = await Context.DocumentTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (documentType == null)
            {
                throw new EntityNotFoundException("Document type not found.");
            }

            if (documentType.System)
            {
                throw new SystemEntityException("System entities cannot be modified");
            }

            documentType.Description = entity.Description;
            documentType.Staff = entity.Staff;
            documentType.Student = entity.Student;
            documentType.Contact = entity.Contact;
            documentType.General = entity.General;
            documentType.Sen = entity.Sen;
        }
    }
}