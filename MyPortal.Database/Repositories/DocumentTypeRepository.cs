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
            transaction)
        {
        }

        public async Task<IEnumerable<DocumentType>> Get(DocumentTypeFilter filter)
        {
            var query = GetDefaultQuery();

            if (filter.Active)
            {
                query.Where($"{TblAlias}.Active", true);
            }

            if (filter.Staff)
            {
                query.Where($"{TblAlias}.Staff", true);
            }

            if (filter.Student)
            {
                query.Where($"{TblAlias}.Student", true);
            }

            if (filter.Contact)
            {
                query.Where($"{TblAlias}.Contact", true);
            }

            if (filter.General)
            {
                query.Where($"{TblAlias}.General", true);
            }

            if (filter.Sen)
            {
                query.Where($"{TblAlias}.Sen", true);
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
                throw ExceptionHelper.UpdateSystemEntityException;
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