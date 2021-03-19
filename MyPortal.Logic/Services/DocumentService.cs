using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Filters;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        public async Task Create(params DocumentModel[] documents)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var document in documents)
                {
                    var directory = await unitOfWork.Directories.GetById(document.DirectoryId);

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

                        unitOfWork.Documents.Create(docToAdd);
                    }
                    catch (Exception e)
                    {
                        throw e.GetBaseException();
                    }
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task Update(params UpdateDocumentModel[] documents)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var document in documents)
                {
                    var documentInDb = await unitOfWork.Documents.GetByIdForEditing(document.Id);

                    documentInDb.Title = document.Title;
                    documentInDb.Description = document.Description;
                    documentInDb.Restricted = document.Restricted;
                    documentInDb.TypeId = document.TypeId;
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DocumentTypeModel>> GetTypes(DocumentTypeFilter filter)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var documentTypes = await unitOfWork.DocumentTypes.Get(filter);

                return documentTypes.Select(BusinessMapper.Map<DocumentTypeModel>).ToList();
            }
        }

        public async Task Delete(params Guid[] documentIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var documentId in documentIds)
                {
                    await unitOfWork.Documents.Delete(documentId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<DocumentModel> GetDocumentById(Guid documentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var document = await unitOfWork.Documents.GetById(documentId);

                if (document == null)
                {
                    throw new NotFoundException("Document not found.");
                }

                return BusinessMapper.Map<DocumentModel>(document);
            }
        }
    }
}
