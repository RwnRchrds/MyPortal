using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Filters;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IDirectoryService _directoryService;

        public DocumentService(IDocumentRepository documentRepository, IDocumentTypeRepository documentTypeRepository,
            IDirectoryService directoryService)
        {
            _documentRepository = documentRepository;
            _documentTypeRepository = documentTypeRepository;
            _directoryService = directoryService;
        }

        public async Task Create(params DocumentModel[] documents)
        {
            foreach (var document in documents)
            {
                var directory = await _directoryService.GetById(document.DirectoryId);

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

                    _documentRepository.Create(docToAdd);
                }
                catch (Exception e)
                {
                    throw e.GetBaseException();
                }
            }

            await _documentRepository.SaveChanges();
        }

        public async Task Update(params UpdateDocumentModel[] documents)
        {
            foreach (var document in documents)
            {
                var documentInDb = await _documentRepository.GetByIdWithTracking(document.Id);

                documentInDb.Title = document.Title;
                documentInDb.Description = document.Description;
                documentInDb.Restricted = document.Restricted;
                documentInDb.TypeId = document.TypeId;
            }

            await _documentRepository.SaveChanges();
        }

        public async Task<IEnumerable<DocumentTypeModel>> GetTypes(DocumentTypeFilter filter)
        {
            var documentTypes = await _documentTypeRepository.Get(filter);

            return documentTypes.Select(BusinessMapper.Map<DocumentTypeModel>).ToList();
        }

        public async Task Delete(params Guid[] documentIds)
        {
            foreach (var documentId in documentIds)
            {
                await _documentRepository.Delete(documentId);
            }

            await _documentRepository.SaveChanges();
        }

        public async Task<DocumentModel> GetDocumentById(Guid documentId)
        {
            var document = await _documentRepository.GetById(documentId);

            if (document == null)
            {
                throw new NotFoundException("Document not found.");
            }

            return BusinessMapper.Map<DocumentModel>(document);
        }

        public override void Dispose()
        {
            _documentRepository.Dispose();
            _documentTypeRepository.Dispose();
            _directoryService.Dispose();
        }
    }
}
