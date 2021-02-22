using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Filters;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        public DocumentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Create(params DocumentModel[] documents)
        {
            foreach (var document in documents)
            {
                var directory = await UnitOfWork.Directories.GetById(document.DirectoryId);

                if (directory == null)
                {
                    throw new NotFoundException("Directory not found.");
                }

                try
                {
                    var docToAdd = new Document
                    {
                        TypeId = document.TypeId,
                        Title = document.Title,
                        Description = document.Description,
                        CreatedDate = DateTime.Today,
                        DirectoryId = document.DirectoryId,
                        CreatedById = document.CreatedById,
                        Deleted = false,
                        Restricted = document.Restricted
                    };

                    UnitOfWork.Documents.Create(docToAdd);
                }
                catch (Exception e)
                {
                    throw e.GetBaseException();
                }
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task Update(params UpdateDocumentModel[] documents)
        {
            foreach (var document in documents)
            {
                var documentInDb = await UnitOfWork.Documents.GetByIdForEditing(document.Id);

                documentInDb.Title = document.Title;
                documentInDb.Description = document.Description;
                documentInDb.Restricted = document.Restricted;
                documentInDb.TypeId = document.TypeId;
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<DocumentTypeModel>> GetTypes(DocumentTypeFilter filter)
        {
            var documentTypes = await UnitOfWork.DocumentTypes.Get(filter);

            return documentTypes.Select(BusinessMapper.Map<DocumentTypeModel>).ToList();
        }

        public async Task Delete(params Guid[] documentIds)
        {
            foreach (var documentId in documentIds)
            {
                await UnitOfWork.Documents.Delete(documentId);
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task<DocumentModel> GetDocumentById(Guid documentId)
        {
            var document = await UnitOfWork.Documents.GetById(documentId);

            if (document == null)
            {
                throw new NotFoundException("Document not found.");
            }

            return BusinessMapper.Map<DocumentModel>(document);
        }
    }
}
